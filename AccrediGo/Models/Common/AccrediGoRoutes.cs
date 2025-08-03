namespace AccrediGo.Models.Common
{
    public static class AccrediGoRoutes
    {
        public static class Auth
        {
            public const string Base = "api/auth";
            public const string Login = "api/auth/login";
            public const string Refresh = "api/auth/refresh";
            public const string Me = "api/auth/me";
            public const string Logout = "api/auth/logout";
            public const string ForgotPassword = "api/auth/forgot-password";
            public const string ResetPassword = "api/auth/reset-password";
            public const string ChangePassword = "api/auth/change-password";
            public const string VerifyEmail = "api/auth/verify-email";
            public const string ResendVerification = "api/auth/resend-verification";
        }

        public static class UserManagement
        {
            public const string Base = "api/user-management";
            public const string Users = "api/user-management/users";
            public const string UserProfiles = "api/user-management/user-profiles";
            public const string UserRoles = "api/user-management/user-roles";
            public const string UserPermissions = "api/user-management/user-permissions";
            public const string UserSessions = "api/user-management/user-sessions";
            public const string UserAuditLogs = "api/user-management/user-audit-logs";
            public const string UserPreferences = "api/user-management/user-preferences";
            public const string UserNotifications = "api/user-management/user-notifications";
            public const string UserDevices = "api/user-management/user-devices";
            public const string UserLocations = "api/user-management/user-locations";
        }

        public static class BillingDetails
        {
            public const string Base = "api/billing-details";
            public const string SubscriptionPlans = "api/billing-details/subscription-plans";
            public const string Subscriptions = "api/billing-details/subscriptions";
            public const string Payments = "api/billing-details/payments";
            public const string PaymentMethods = "api/billing-details/payment-methods";
            public const string Invoices = "api/billing-details/invoices";
            public const string InvoiceItems = "api/billing-details/invoice-items";
            public const string Discounts = "api/billing-details/discounts";
            public const string TaxRates = "api/billing-details/tax-rates";
            public const string BillingCycles = "api/billing-details/billing-cycles";
            public const string PaymentSchedules = "api/billing-details/payment-schedules";
            public const string Refunds = "api/billing-details/refunds";
            public const string PaymentHistory = "api/billing-details/payment-history";
        }

        public static class Accreditation
        {
            public const string Base = "api/accreditation";
            public const string Accreditations = "api/accreditation/accreditations";
            public const string AccreditationStandards = "api/accreditation/standards";
            public const string AccreditationChapters = "api/accreditation/chapters";
            public const string AccreditationQuestions = "api/accreditation/questions";
            public const string AccreditationAnswers = "api/accreditation/answers";
            public const string AccreditationSessions = "api/accreditation/sessions";
            public const string AccreditationReports = "api/accreditation/reports";
            public const string AccreditationTemplates = "api/accreditation/templates";
            public const string AccreditationWorkflows = "api/accreditation/workflows";
            public const string AccreditationAudits = "api/accreditation/audits";
            public const string AccreditationCompliance = "api/accreditation/compliance";
            public const string AccreditationMetrics = "api/accreditation/metrics";
        }

        public static class FacilityManagement
        {
            public const string Base = "api/facility-management";
            public const string Facilities = "api/facility-management/facilities";
            public const string FacilityTypes = "api/facility-management/facility-types";
            public const string FacilityRoles = "api/facility-management/facility-roles";
            public const string FacilityUsers = "api/facility-management/facility-users";
            public const string FacilityPermissions = "api/facility-management/facility-permissions";
            public const string FacilitySettings = "api/facility-management/facility-settings";
            public const string FacilityAudits = "api/facility-management/facility-audits";
            public const string FacilityReports = "api/facility-management/facility-reports";
            public const string FacilityMetrics = "api/facility-management/facility-metrics";
            public const string FacilityNotifications = "api/facility-management/facility-notifications";
        }

        public static class SessionManagement
        {
            public const string Base = "api/session-management";
            public const string GapAnalysisSessions = "api/session-management/gap-analysis-sessions";
            public const string SessionComponents = "api/session-management/session-components";
            public const string ActionPlanComponents = "api/session-management/action-plan-components";
            public const string SessionProgress = "api/session-management/session-progress";
            public const string SessionReports = "api/session-management/session-reports";
            public const string SessionTemplates = "api/session-management/session-templates";
            public const string SessionWorkflows = "api/session-management/session-workflows";
            public const string SessionAudits = "api/session-management/session-audits";
            public const string SessionMetrics = "api/session-management/session-metrics";
        }

        public static class SystemManagement
        {
            public const string Base = "api/system-management";
            public const string SystemRoles = "api/system-management/system-roles";
            public const string SystemPermissions = "api/system-management/system-permissions";
            public const string SystemSettings = "api/system-management/system-settings";
            public const string SystemLogs = "api/system-management/system-logs";
            public const string SystemAudits = "api/system-management/system-audits";
            public const string SystemReports = "api/system-management/system-reports";
            public const string SystemMetrics = "api/system-management/system-metrics";
            public const string SystemNotifications = "api/system-management/system-notifications";
            public const string SystemBackups = "api/system-management/system-backups";
            public const string SystemMaintenance = "api/system-management/system-maintenance";
        }

        public static class Reporting
        {
            public const string Base = "api/reporting";
            public const string AccreditationReports = "api/reporting/accreditation-reports";
            public const string ComplianceReports = "api/reporting/compliance-reports";
            public const string UserReports = "api/reporting/user-reports";
            public const string FacilityReports = "api/reporting/facility-reports";
            public const string SessionReports = "api/reporting/session-reports";
            public const string BillingReports = "api/reporting/billing-reports";
            public const string AuditReports = "api/reporting/audit-reports";
            public const string PerformanceReports = "api/reporting/performance-reports";
            public const string AnalyticsReports = "api/reporting/analytics-reports";
            public const string ExportReports = "api/reporting/export-reports";
        }

        public static class Notifications
        {
            public const string Base = "api/notifications";
            public const string UserNotifications = "api/notifications/user-notifications";
            public const string SystemNotifications = "api/notifications/system-notifications";
            public const string EmailNotifications = "api/notifications/email-notifications";
            public const string SMSNotifications = "api/notifications/sms-notifications";
            public const string PushNotifications = "api/notifications/push-notifications";
            public const string NotificationTemplates = "api/notifications/notification-templates";
            public const string NotificationSettings = "api/notifications/notification-settings";
            public const string NotificationHistory = "api/notifications/notification-history";
            public const string NotificationPreferences = "api/notifications/notification-preferences";
        }

        public static class FileManagement
        {
            public const string Base = "api/file-management";
            public const string FileUploads = "api/file-management/file-uploads";
            public const string FileDownloads = "api/file-management/file-downloads";
            public const string FileStorage = "api/file-management/file-storage";
            public const string FileSharing = "api/file-management/file-sharing";
            public const string FilePermissions = "api/file-management/file-permissions";
            public const string FileAudits = "api/file-management/file-audits";
            public const string FileBackups = "api/file-management/file-backups";
            public const string FileArchives = "api/file-management/file-archives";
            public const string FileTemplates = "api/file-management/file-templates";
        }

        public static class Integration
        {
            public const string Base = "api/integration";
            public const string ExternalAPIs = "api/integration/external-apis";
            public const string Webhooks = "api/integration/webhooks";
            public const string DataSync = "api/integration/data-sync";
            public const string ImportExport = "api/integration/import-export";
            public const string ThirdPartyIntegrations = "api/integration/third-party-integrations";
            public const string APIKeys = "api/integration/api-keys";
            public const string IntegrationLogs = "api/integration/integration-logs";
            public const string IntegrationSettings = "api/integration/integration-settings";
        }

        public static class Analytics
        {
            public const string Base = "api/analytics";
            public const string UserAnalytics = "api/analytics/user-analytics";
            public const string FacilityAnalytics = "api/analytics/facility-analytics";
            public const string AccreditationAnalytics = "api/analytics/accreditation-analytics";
            public const string PerformanceAnalytics = "api/analytics/performance-analytics";
            public const string UsageAnalytics = "api/analytics/usage-analytics";
            public const string ComplianceAnalytics = "api/analytics/compliance-analytics";
            public const string RevenueAnalytics = "api/analytics/revenue-analytics";
            public const string CustomAnalytics = "api/analytics/custom-analytics";
            public const string AnalyticsDashboards = "api/analytics/analytics-dashboards";
        }

        public static class Health
        {
            public const string Base = "api/health";
            public const string HealthCheck = "api/health/health-check";
            public const string SystemStatus = "api/health/system-status";
            public const string DatabaseStatus = "api/health/database-status";
            public const string ServiceStatus = "api/health/service-status";
            public const string PerformanceMetrics = "api/health/performance-metrics";
            public const string ErrorLogs = "api/health/error-logs";
            public const string SystemResources = "api/health/system-resources";
        }

        public static class AutoComplete
        {
            public const string Base = "api/autocomplete";
            public const string Users = "api/autocomplete/users";
            public const string Facilities = "api/autocomplete/facilities";
            public const string AccreditationStandards = "api/autocomplete/accreditation-standards";
            public const string AccreditationChapters = "api/autocomplete/accreditation-chapters";
            public const string SubscriptionPlans = "api/autocomplete/subscription-plans";
            public const string SystemRoles = "api/autocomplete/system-roles";
            public const string FacilityTypes = "api/autocomplete/facility-types";
            public const string PaymentMethods = "api/autocomplete/payment-methods";
            public const string NotificationTypes = "api/autocomplete/notification-types";
        }

        public static class Admin
        {
            public const string Base = "api/admin";
            public const string SystemConfiguration = "api/admin/system-configuration";
            public const string UserManagement = "api/admin/user-management";
            public const string RoleManagement = "api/admin/role-management";
            public const string PermissionManagement = "api/admin/permission-management";
            public const string FacilityManagement = "api/admin/facility-management";
            public const string BillingManagement = "api/admin/billing-management";
            public const string AccreditationManagement = "api/admin/accreditation-management";
            public const string ReportingManagement = "api/admin/reporting-management";
            public const string NotificationManagement = "api/admin/notification-management";
            public const string IntegrationManagement = "api/admin/integration-management";
            public const string AnalyticsManagement = "api/admin/analytics-management";
            public const string SystemMaintenance = "api/admin/system-maintenance";
            public const string BackupRestore = "api/admin/backup-restore";
            public const string AuditLogs = "api/admin/audit-logs";
            public const string SystemMetrics = "api/admin/system-metrics";
        }
    }
} 