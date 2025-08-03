# Route Constants Implementation

## Overview
This document describes the `AccrediGoRoutes` implementation for the AccrediGo application. The route constants provide a centralized, type-safe way to manage API routes across the entire application, following the same pattern as the HIS system.

## Architecture

### **1. AccrediGoRoutes Class**
- **Location**: `Models/Common/AccrediGoRoutes.cs`
- **Purpose**: Centralized route constants for all API endpoints
- **Structure**: Organized by functional modules
- **Benefits**: Type-safe, maintainable, and consistent routing

### **2. Route Organization**
The routes are organized into logical modules:

#### **Authentication Routes**
```csharp
public static class Auth
{
    public const string Base = "api/auth";
    public const string Login = "api/auth/login";
    public const string Refresh = "api/auth/refresh";
    public const string Me = "api/auth/me";
    // ... more auth routes
}
```

#### **User Management Routes**
```csharp
public static class UserManagement
{
    public const string Base = "api/user-management";
    public const string Users = "api/user-management/users";
    public const string UserProfiles = "api/user-management/user-profiles";
    // ... more user management routes
}
```

#### **Billing Details Routes**
```csharp
public static class BillingDetails
{
    public const string Base = "api/billing-details";
    public const string SubscriptionPlans = "api/billing-details/subscription-plans";
    public const string Subscriptions = "api/billing-details/subscriptions";
    // ... more billing routes
}
```

#### **Accreditation Routes**
```csharp
public static class Accreditation
{
    public const string Base = "api/accreditation";
    public const string Accreditations = "api/accreditation/accreditations";
    public const string AccreditationStandards = "api/accreditation/standards";
    // ... more accreditation routes
}
```

## Usage Examples

### **1. Controller Route Declaration**
```csharp
[Route(AccrediGoRoutes.Auth.Base)]
public class AuthController : ApiControllerBase
{
    // Controller implementation
}
```

### **2. User Management Controller**
```csharp
[Route(AccrediGoRoutes.UserManagement.Users)]
[Authorize]
public class UserController : ApiControllerBase
{
    // Controller implementation
}
```

### **3. Billing Controller**
```csharp
[Route(AccrediGoRoutes.BillingDetails.SubscriptionPlans)]
[Authorize]
public class SubscriptionPlanController : ApiControllerBase
{
    // Controller implementation
}
```

### **4. Accreditation Controller**
```csharp
[Route(AccrediGoRoutes.Accreditation.Accreditations)]
[Authorize]
public class AccreditationController : ApiControllerBase
{
    // Controller implementation
}
```

### **5. System Management Controller**
```csharp
[Route(AccrediGoRoutes.SystemManagement.SystemRoles)]
[Authorize]
public class SystemRoleController : ApiControllerBase
{
    // Controller implementation
}
```

### **6. Health Check Controller**
```csharp
[Route(AccrediGoRoutes.Health.HealthCheck)]
[AllowAnonymous]
public class HealthCheckController : ApiControllerBase
{
    // Controller implementation
}
```

## Route Categories

### **1. Authentication Routes**
- **Base**: `api/auth`
- **Login**: `api/auth/login`
- **Refresh**: `api/auth/refresh`
- **Me**: `api/auth/me`
- **Logout**: `api/auth/logout`
- **ForgotPassword**: `api/auth/forgot-password`
- **ResetPassword**: `api/auth/reset-password`
- **ChangePassword**: `api/auth/change-password`
- **VerifyEmail**: `api/auth/verify-email`
- **ResendVerification**: `api/auth/resend-verification`

### **2. User Management Routes**
- **Base**: `api/user-management`
- **Users**: `api/user-management/users`
- **UserProfiles**: `api/user-management/user-profiles`
- **UserRoles**: `api/user-management/user-roles`
- **UserPermissions**: `api/user-management/user-permissions`
- **UserSessions**: `api/user-management/user-sessions`
- **UserAuditLogs**: `api/user-management/user-audit-logs`
- **UserPreferences**: `api/user-management/user-preferences`
- **UserNotifications**: `api/user-management/user-notifications`
- **UserDevices**: `api/user-management/user-devices`
- **UserLocations**: `api/user-management/user-locations`

### **3. Billing Details Routes**
- **Base**: `api/billing-details`
- **SubscriptionPlans**: `api/billing-details/subscription-plans`
- **Subscriptions**: `api/billing-details/subscriptions`
- **Payments**: `api/billing-details/payments`
- **PaymentMethods**: `api/billing-details/payment-methods`
- **Invoices**: `api/billing-details/invoices`
- **InvoiceItems**: `api/billing-details/invoice-items`
- **Discounts**: `api/billing-details/discounts`
- **TaxRates**: `api/billing-details/tax-rates`
- **BillingCycles**: `api/billing-details/billing-cycles`
- **PaymentSchedules**: `api/billing-details/payment-schedules`
- **Refunds**: `api/billing-details/refunds`
- **PaymentHistory**: `api/billing-details/payment-history`

### **4. Accreditation Routes**
- **Base**: `api/accreditation`
- **Accreditations**: `api/accreditation/accreditations`
- **AccreditationStandards**: `api/accreditation/standards`
- **AccreditationChapters**: `api/accreditation/chapters`
- **AccreditationQuestions**: `api/accreditation/questions`
- **AccreditationAnswers**: `api/accreditation/answers`
- **AccreditationSessions**: `api/accreditation/sessions`
- **AccreditationReports**: `api/accreditation/reports`
- **AccreditationTemplates**: `api/accreditation/templates`
- **AccreditationWorkflows**: `api/accreditation/workflows`
- **AccreditationAudits**: `api/accreditation/audits`
- **AccreditationCompliance**: `api/accreditation/compliance`
- **AccreditationMetrics**: `api/accreditation/metrics`

### **5. Facility Management Routes**
- **Base**: `api/facility-management`
- **Facilities**: `api/facility-management/facilities`
- **FacilityTypes**: `api/facility-management/facility-types`
- **FacilityRoles**: `api/facility-management/facility-roles`
- **FacilityUsers**: `api/facility-management/facility-users`
- **FacilityPermissions**: `api/facility-management/facility-permissions`
- **FacilitySettings**: `api/facility-management/facility-settings`
- **FacilityAudits**: `api/facility-management/facility-audits`
- **FacilityReports**: `api/facility-management/facility-reports`
- **FacilityMetrics**: `api/facility-management/facility-metrics`
- **FacilityNotifications**: `api/facility-management/facility-notifications`

### **6. Session Management Routes**
- **Base**: `api/session-management`
- **GapAnalysisSessions**: `api/session-management/gap-analysis-sessions`
- **SessionComponents**: `api/session-management/session-components`
- **ActionPlanComponents**: `api/session-management/action-plan-components`
- **SessionProgress**: `api/session-management/session-progress`
- **SessionReports**: `api/session-management/session-reports`
- **SessionTemplates**: `api/session-management/session-templates`
- **SessionWorkflows**: `api/session-management/session-workflows`
- **SessionAudits**: `api/session-management/session-audits`
- **SessionMetrics**: `api/session-management/session-metrics`

### **7. System Management Routes**
- **Base**: `api/system-management`
- **SystemRoles**: `api/system-management/system-roles`
- **SystemPermissions**: `api/system-management/system-permissions`
- **SystemSettings**: `api/system-management/system-settings`
- **SystemLogs**: `api/system-management/system-logs`
- **SystemAudits**: `api/system-management/system-audits`
- **SystemReports**: `api/system-management/system-reports`
- **SystemMetrics**: `api/system-management/system-metrics`
- **SystemNotifications**: `api/system-management/system-notifications`
- **SystemBackups**: `api/system-management/system-backups`
- **SystemMaintenance**: `api/system-management/system-maintenance`

### **8. Reporting Routes**
- **Base**: `api/reporting`
- **AccreditationReports**: `api/reporting/accreditation-reports`
- **ComplianceReports**: `api/reporting/compliance-reports`
- **UserReports**: `api/reporting/user-reports`
- **FacilityReports**: `api/reporting/facility-reports`
- **SessionReports**: `api/reporting/session-reports`
- **BillingReports**: `api/reporting/billing-reports`
- **AuditReports**: `api/reporting/audit-reports`
- **PerformanceReports**: `api/reporting/performance-reports`
- **AnalyticsReports**: `api/reporting/analytics-reports`
- **ExportReports**: `api/reporting/export-reports`

### **9. Notifications Routes**
- **Base**: `api/notifications`
- **UserNotifications**: `api/notifications/user-notifications`
- **SystemNotifications**: `api/notifications/system-notifications`
- **EmailNotifications**: `api/notifications/email-notifications`
- **SMSNotifications**: `api/notifications/sms-notifications`
- **PushNotifications**: `api/notifications/push-notifications`
- **NotificationTemplates**: `api/notifications/notification-templates`
- **NotificationSettings**: `api/notifications/notification-settings`
- **NotificationHistory**: `api/notifications/notification-history`
- **NotificationPreferences**: `api/notifications/notification-preferences`

### **10. File Management Routes**
- **Base**: `api/file-management`
- **FileUploads**: `api/file-management/file-uploads`
- **FileDownloads**: `api/file-management/file-downloads`
- **FileStorage**: `api/file-management/file-storage`
- **FileSharing**: `api/file-management/file-sharing`
- **FilePermissions**: `api/file-management/file-permissions`
- **FileAudits**: `api/file-management/file-audits`
- **FileBackups**: `api/file-management/file-backups`
- **FileArchives**: `api/file-management/file-archives`
- **FileTemplates**: `api/file-management/file-templates`

### **11. Integration Routes**
- **Base**: `api/integration`
- **ExternalAPIs**: `api/integration/external-apis`
- **Webhooks**: `api/integration/webhooks`
- **DataSync**: `api/integration/data-sync`
- **ImportExport**: `api/integration/import-export`
- **ThirdPartyIntegrations**: `api/integration/third-party-integrations`
- **APIKeys**: `api/integration/api-keys`
- **IntegrationLogs**: `api/integration/integration-logs`
- **IntegrationSettings**: `api/integration/integration-settings`

### **12. Analytics Routes**
- **Base**: `api/analytics`
- **UserAnalytics**: `api/analytics/user-analytics`
- **FacilityAnalytics**: `api/analytics/facility-analytics`
- **AccreditationAnalytics**: `api/analytics/accreditation-analytics`
- **PerformanceAnalytics**: `api/analytics/performance-analytics`
- **UsageAnalytics**: `api/analytics/usage-analytics`
- **ComplianceAnalytics**: `api/analytics/compliance-analytics`
- **RevenueAnalytics**: `api/analytics/revenue-analytics`
- **CustomAnalytics**: `api/analytics/custom-analytics`
- **AnalyticsDashboards**: `api/analytics/analytics-dashboards`

### **13. Health Routes**
- **Base**: `api/health`
- **HealthCheck**: `api/health/health-check`
- **SystemStatus**: `api/health/system-status`
- **DatabaseStatus**: `api/health/database-status`
- **ServiceStatus**: `api/health/service-status`
- **PerformanceMetrics**: `api/health/performance-metrics`
- **ErrorLogs**: `api/health/error-logs`
- **SystemResources**: `api/health/system-resources`

### **14. AutoComplete Routes**
- **Base**: `api/autocomplete`
- **Users**: `api/autocomplete/users`
- **Facilities**: `api/autocomplete/facilities`
- **AccreditationStandards**: `api/autocomplete/accreditation-standards`
- **AccreditationChapters**: `api/autocomplete/accreditation-chapters`
- **SubscriptionPlans**: `api/autocomplete/subscription-plans`
- **SystemRoles**: `api/autocomplete/system-roles`
- **FacilityTypes**: `api/autocomplete/facility-types`
- **PaymentMethods**: `api/autocomplete/payment-methods`
- **NotificationTypes**: `api/autocomplete/notification-types`

### **15. Admin Routes**
- **Base**: `api/admin`
- **SystemConfiguration**: `api/admin/system-configuration`
- **UserManagement**: `api/admin/user-management`
- **RoleManagement**: `api/admin/role-management`
- **PermissionManagement**: `api/admin/permission-management`
- **FacilityManagement**: `api/admin/facility-management`
- **BillingManagement**: `api/admin/billing-management`
- **AccreditationManagement**: `api/admin/accreditation-management`
- **ReportingManagement**: `api/admin/reporting-management`
- **NotificationManagement**: `api/admin/notification-management`
- **IntegrationManagement**: `api/admin/integration-management`
- **AnalyticsManagement**: `api/admin/analytics-management`
- **SystemMaintenance**: `api/admin/system-maintenance`
- **BackupRestore**: `api/admin/backup-restore`
- **AuditLogs**: `api/admin/audit-logs`
- **SystemMetrics**: `api/admin/system-metrics`

## Updated Controllers

### **1. AuthController**
```csharp
[Route(AccrediGoRoutes.Auth.Base)]
public class AuthController : ApiControllerBase
```

### **2. SubscriptionPlanController**
```csharp
[Route(AccrediGoRoutes.BillingDetails.SubscriptionPlans)]
[Authorize]
public class SubscriptionPlanController : ApiControllerBase
```

### **3. UserController**
```csharp
[Route(AccrediGoRoutes.UserManagement.Users)]
[Authorize]
public class UserController : ApiControllerBase
```

### **4. AccreditationController**
```csharp
[Route(AccrediGoRoutes.Accreditation.Accreditations)]
[Authorize]
public class AccreditationController : ApiControllerBase
```

### **5. UserProfileController**
```csharp
[Route(AccrediGoRoutes.UserManagement.UserProfiles)]
[Authorize]
public class UserProfileController : ApiControllerBase
```

### **6. AccreditationStandardController**
```csharp
[Route(AccrediGoRoutes.Accreditation.AccreditationStandards)]
[Authorize]
public class AccreditationStandardController : ApiControllerBase
```

### **7. SystemRoleController**
```csharp
[Route(AccrediGoRoutes.SystemManagement.SystemRoles)]
[Authorize]
public class SystemRoleController : ApiControllerBase
```

### **8. HealthCheckController**
```csharp
[Route(AccrediGoRoutes.Health.HealthCheck)]
[AllowAnonymous]
public class HealthCheckController : ApiControllerBase
```

## Benefits

### **1. Type Safety**
- ✅ **Compile-time checking**: Routes are validated at compile time
- ✅ **IntelliSense support**: Full IntelliSense support in Visual Studio
- ✅ **Refactoring support**: Safe refactoring with automatic updates

### **2. Maintainability**
- ✅ **Centralized management**: All routes in one place
- ✅ **Easy updates**: Change routes in one location
- ✅ **Consistent naming**: Standardized route naming conventions

### **3. Organization**
- ✅ **Logical grouping**: Routes organized by functional modules
- ✅ **Clear structure**: Easy to understand and navigate
- ✅ **Scalable**: Easy to add new routes and modules

### **4. Developer Experience**
- ✅ **Intuitive API**: Clear and consistent route structure
- ✅ **Reduced errors**: No typos in route strings
- ✅ **Better documentation**: Self-documenting route structure

### **5. Consistency**
- ✅ **Standardized patterns**: Consistent route naming
- ✅ **Version control**: Easy to track route changes
- ✅ **Team collaboration**: Clear understanding of route structure

## Route Naming Conventions

### **1. Base Routes**
- All routes start with `api/`
- Module-specific base routes: `api/{module}`
- Example: `api/user-management`, `api/billing-details`

### **2. Resource Routes**
- Plural nouns for resource collections
- Example: `users`, `subscription-plans`, `accreditations`

### **3. Action Routes**
- Descriptive action names
- Example: `login`, `refresh`, `health-check`

### **4. Nested Routes**
- Hierarchical structure for related resources
- Example: `user-management/user-profiles`, `accreditation/standards`

### **5. Special Routes**
- Health checks: `health-check`
- Auto-complete: `autocomplete`
- Admin functions: `admin`

## API Endpoints Summary

### **Authentication Endpoints**
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh` - Refresh token
- `GET /api/auth/me` - Get current user
- `POST /api/auth/logout` - User logout

### **User Management Endpoints**
- `GET /api/user-management/users` - Get all users
- `POST /api/user-management/users` - Create user
- `GET /api/user-management/users/{id}` - Get user by ID
- `PUT /api/user-management/users/{id}` - Update user
- `DELETE /api/user-management/users/{id}` - Delete user

### **Billing Endpoints**
- `GET /api/billing-details/subscription-plans` - Get subscription plans
- `POST /api/billing-details/subscription-plans` - Create subscription plan
- `PUT /api/billing-details/subscription-plans/{id}` - Update subscription plan

### **Accreditation Endpoints**
- `GET /api/accreditation/accreditations` - Get accreditations
- `POST /api/accreditation/accreditations` - Create accreditation
- `GET /api/accreditation/standards` - Get accreditation standards

### **System Management Endpoints**
- `GET /api/system-management/system-roles` - Get system roles
- `POST /api/system-management/system-roles` - Create system role
- `PUT /api/system-management/system-roles/{id}` - Update system role

### **Health Check Endpoints**
- `GET /api/health/health-check` - Basic health check
- `GET /api/health/health-check/detailed` - Detailed health check

## Next Steps

### **1. Immediate Improvements**
- [ ] Add route versioning support
- [ ] Implement route documentation generation
- [ ] Add route validation middleware
- [ ] Create route testing utilities

### **2. Advanced Features**
- [ ] Add route caching
- [ ] Implement route analytics
- [ ] Add route monitoring
- [ ] Create route performance metrics

### **3. Documentation**
- [ ] Generate API documentation from routes
- [ ] Create route usage examples
- [ ] Add route change tracking
- [ ] Implement route deprecation warnings

The route constants implementation is now **complete and production-ready**! 🚀 