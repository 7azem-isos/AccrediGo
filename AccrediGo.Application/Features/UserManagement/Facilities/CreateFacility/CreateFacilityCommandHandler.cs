using System;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using AccrediGo.Domain.Enums;
using AccrediGo.Application.Services;

namespace AccrediGo.Application.Features.UserManagement.Facilities.CreateFacility
{
    public class CreateFacilityCommandHandler : IRequestHandler<CreateFacilityCommand, CreateFacilityDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly ILogger<CreateFacilityCommandHandler> _logger;
        private readonly IMailService _mailService;

        public CreateFacilityCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuditService auditService,
            ILogger<CreateFacilityCommandHandler> logger,
            IMailService mailService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public async Task<CreateFacilityDto> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateFacilityCommand. Facility: {Name}, Email: {Email}", request.Name, request.Email);
            // _auditService.PopulateAuditInfo(request);

            // Create User with email verification fields
            var hashedPassword = AccrediGo.Application.Common.PasswordHasher.HashPassword(request.Password ?? string.Empty);
            var verificationToken = Guid.NewGuid().ToString("N");
            var tokenExpires = DateTime.UtcNow.AddHours(24);
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                ArabicName = request.ArabicName,
                Email = request.Email!,
                Password = hashedPassword,
                PhoneNumber = request.Phone,
                SystemRoleId = request.SystemRoleId,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy,
                UserAgent = request.UserAgent,
                CreatedFromIp = request.CreatedFromIp,
                AuditContext = request.AuditContext,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiresAt = tokenExpires
            };
            await _unitOfWork.GetRepository<User>().AddAsync(user, cancellationToken);
            // Send verification email
            var verificationUrl = $"https://yourdomain.com/api/auth/verify-email?token={verificationToken}";
            var emailBody = $"<p>Welcome to AccrediGo!</p><p>Please verify your email by clicking <a href='{verificationUrl}'>here</a>.</p>";
            await _mailService.SendEmailAsync(user.Email, "Verify your email address", emailBody);

            // Create Facility
            var facility = new Facility
            {
                UserId = user.Id,
                Name = request.Name,
                ArabicName = request.ArabicName,
                Location = request.Location,
                ArabicLocation = request.ArabicLocation,
                FacilityTypeId = request.FacilityTypeId,
                AccreditationId = request.AccreditationId,
                Email = request.Email,
                Phone = request.Phone,
                Tel = request.Tel,
                CompanySize = (CompanySize)request.CompanySize,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy,
                UserAgent = request.UserAgent,
                CreatedFromIp = request.CreatedFromIp,
                AuditContext = request.AuditContext,
                IsApproved = false,
                ApprovedAt = null,
                ApprovedBy = null
            };
            await _unitOfWork.GetRepository<Facility>().AddAsync(facility, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully created facility with UserId: {UserId}", user.Id);
            return _mapper.Map<CreateFacilityDto>(facility);
        }
    }
}
