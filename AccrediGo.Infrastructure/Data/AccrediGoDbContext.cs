using Microsoft.EntityFrameworkCore;
using AccrediGo.Domain.Entities.BaseModels;
using AccrediGo.Domain.Entities.BillingDetails;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.Roles;
using AccrediGo.Domain.Entities.SessionDetails;
using AccrediGo.Domain.Entities.UserDetails;

namespace AccrediGo.Infrastructure.Data
{
    public class AccrediGoDbContext : DbContext
    {
        public AccrediGoDbContext(DbContextOptions<AccrediGoDbContext> options)
            : base(options)
        {
        }

        // BillingDetails
        public DbSet<Feature> Features { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<SubscriptionPlanFeature> SubscriptionPlanFeatures { get; set; }

        // MainComponents
        public DbSet<Accreditation> Accreditations { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterAccreditationFacilityType> ChapterAccreditationFacilityTypes { get; set; }
        public DbSet<EoC> EoCs { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<ImprovementScenario> ImprovementScenarios { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Standard> Standards { get; set; }

        // Roles
        public DbSet<FacilityRole> FacilityRoles { get; set; }
        public DbSet<FacilityRolePermission> FacilityRolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<SystemRolePermission> SystemRolePermissions { get; set; }

        // SessionDetails
        public DbSet<ActionPlanComponent> ActionPlanComponents { get; set; }
        public DbSet<GapAnalysisSession> GapAnalysisSessions { get; set; }
        public DbSet<SessionComponent> SessionComponents { get; set; }

        // UserDetails
        public DbSet<ExploreUserAccess> ExploreUserAccesses { get; set; }
        public DbSet<FacilityUser> FacilityUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActionLog> UserActionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure BaseEntity properties (inherited by all entities)
            //modelBuilder.Entity<BaseEntity>()
            //    .HasQueryFilter(e => !e.IsDeleted);

            // BillingDetails configurations
            modelBuilder.Entity<SubscriptionPlanFeature>()
                .HasOne(spf => spf.SubscriptionPlan)
                .WithMany(sp => sp.SubscriptionPlanFeatures)
                .HasForeignKey(spf => spf.SubscriptionPlanID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubscriptionPlanFeature>()
                .HasOne(spf => spf.Feature)
                .WithMany(f => f.SubscriptionPlanFeatures)
                .HasForeignKey(spf => spf.FeatureID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Facility)
                .WithMany(f => f.Subscriptions)
                .HasForeignKey(s => s.FacilityID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Facility)
                .WithMany(f => f.Payments)
                .HasForeignKey(p => p.FacilityID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SessionComponent>()
                .HasOne(sc => sc.GapAnalysisSession)
                .WithMany(gas => gas.SessionComponents)
                .HasForeignKey(sc => sc.SessionId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            modelBuilder.Entity<SessionComponent>()
                .HasOne(sc => sc.Question)
                .WithMany(q => q.SessionComponents)
                .HasForeignKey(sc => sc.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            modelBuilder.Entity<ActionPlanComponent>()
                .HasOne(apc => apc.GapAnalysisSession)
                .WithMany(gas => gas.ActionPlanComponents)
                .HasForeignKey(apc => apc.SessionId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            modelBuilder.Entity<ActionPlanComponent>()
                .HasOne(apc => apc.ImprovementScenario)
                .WithMany(ic => ic.ActionPlanComponents)
                .HasForeignKey(apc => apc.ScenarioId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            modelBuilder.Entity<ActionPlanComponent>()
                .HasOne(apc => apc.FacilityUser)
                .WithMany(fUser => fUser.ActionPlanComponents)
                .HasForeignKey(apc => apc.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            // MainComponents configurations
            modelBuilder.Entity<AnswerOption>()
                .HasOne(ao => ao.ImprovementScenario)
                .WithOne(improvementScenario => improvementScenario.AnswerOption)
                .HasForeignKey<ImprovementScenario>(ao => ao.AnswerOptionId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Question>()
                .HasOne(q => q.DependsOnQuestion)
                .WithMany()
                .HasForeignKey(q => q.DependsOnQuestionId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Facility>()
                .HasOne(f => f.Accreditation)
                .WithMany(a => a.Facilities)
                .HasForeignKey(f => f.AccreditationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FacilityRolePermission>()
                .HasKey(frp => new { frp.FacilityRoleID, frp.PermissionID });

            modelBuilder.Entity<SystemRolePermission>()
                .HasKey(srp => new { srp.SystemRoleID, srp.PermissionID });

            // Indexes for performance
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<FacilityUser>()
                .HasIndex(fu => fu.FacilityID);

            modelBuilder.Entity<Subscription>()
                .HasIndex(s => s.FacilityID);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.SubscriptionID);
        }
    }
}