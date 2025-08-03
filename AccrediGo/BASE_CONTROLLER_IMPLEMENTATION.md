# Base Controller Implementation

## Overview
This document describes the `ApiControllerBase` implementation for the AccrediGo application. The base controller provides common validation methods, current request context, and standardized error handling for all API controllers.

## Architecture

### **1. ApiControllerBase Class**
- **Location**: `Controllers/Base/ApiControllerBase.cs`
- **Purpose**: Provides common functionality for all API controllers
- **Features**:
  - Model state validation
  - List validation
  - String validation
  - Null validation
  - Condition validation
  - Current request context access

### **2. ICurrentRequest Interface**
- **Location**: `Models/Common/ICurrentRequest.cs`
- **Purpose**: Provides access to current request context
- **Properties**:
  - `Lang` - Current language (en/ar)
  - `UserId` - Current user ID
  - `UserName` - Current user name
  - `UserEmail` - Current user email
  - `UserRoleId` - Current user role ID
  - `CorrelationId` - Request correlation ID
  - `RequestTime` - Request timestamp

### **3. CurrentRequest Implementation**
- **Location**: `Models/Common/CurrentRequest.cs`
- **Purpose**: Implements ICurrentRequest using HttpContext
- **Features**:
  - Extracts user claims from JWT token
  - Reads Accept-Language header
  - Provides request correlation ID
  - Handles null/empty values gracefully

### **4. BusinessValidationException**
- **Location**: `Models/Common/BusinessValidationException.cs`
- **Purpose**: Custom exception for business validation errors
- **Features**:
  - Message code for error categorization
  - Localized error messages
  - Inner exception support

## Usage Examples

### **1. Controller Inheritance**
```csharp
[Route("api/[controller]")]
[Authorize]
public class UserController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator, ICurrentRequest currentRequest) : base(currentRequest)
    {
        _mediator = mediator;
    }
}
```

### **2. Model State Validation**
```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
{
    try
    {
        // Validate model state with localized messages
        ValidateModelState("USER_CREATE_VALIDATION_ERROR", 
            "Invalid user data", 
            "بيانات المستخدم غير صالحة");

        var result = await _mediator.Send(command);
        return Ok(ApiResponse<CreateUserDto>.Success(result, "User Created Successfully"));
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<CreateUserDto>.ValidationError(ex.Message));
    }
    catch (Exception ex)
    {
        return BadRequest(ApiResponse<CreateUserDto>.Error("Failed to create user", ex));
    }
}
```

### **3. List Validation**
```csharp
[HttpPost("bulk")]
public async Task<IActionResult> CreateBulk([FromBody] List<CreateUserCommand> commands)
{
    try
    {
        // Validate that the list is not empty
        ValidateListNotEmpty(commands, "USER_BULK_EMPTY_ERROR",
            "The user list cannot be empty",
            "قائمة المستخدمين لا يمكن أن تكون فارغة");

        var results = new List<CreateUserDto>();
        foreach (var command in commands)
        {
            var result = await _mediator.Send(command);
            results.Add(result);
        }

        return Ok(ApiResponse<List<CreateUserDto>>.Success(results, "Users Created Successfully"));
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<List<CreateUserDto>>.ValidationError(ex.Message));
    }
}
```

### **4. String Validation**
```csharp
[HttpGet("search")]
public async Task<IActionResult> Search([FromQuery] string searchTerm)
{
    try
    {
        // Validate search term is not empty
        ValidateStringNotEmpty(searchTerm, "SEARCH_TERM_EMPTY_ERROR",
            "Search term cannot be empty",
            "مصطلح البحث لا يمكن أن يكون فارغاً");

        var query = new SearchUsersQuery { SearchTerm = searchTerm };
        var result = await _mediator.Send(query);
        return Ok(ApiResponse<IEnumerable<UserDto>>.Success(result, "Search completed"));
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<IEnumerable<UserDto>>.ValidationError(ex.Message));
    }
}
```

### **5. Null Validation**
```csharp
[HttpPut("{id}")]
public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command)
{
    try
    {
        // Validate command is not null
        ValidateNotNull(command, "UPDATE_COMMAND_NULL_ERROR",
            "Update command cannot be null",
            "أمر التحديث لا يمكن أن يكون فارغاً");

        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<UpdateUserDto>.Success(result, "User Updated Successfully"));
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<UpdateUserDto>.ValidationError(ex.Message));
    }
}
```

### **6. Condition Validation**
```csharp
[HttpPost("activate")]
public async Task<IActionResult> ActivateUser(string id)
{
    try
    {
        // Validate user has permission to activate users
        ValidateCondition(GetCurrentUserRoleId() >= 2, "INSUFFICIENT_PERMISSIONS_ERROR",
            "You don't have permission to activate users",
            "ليس لديك صلاحية لتفعيل المستخدمين");

        var command = new ActivateUserCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<bool>.Success(result, "User Activated Successfully"));
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<bool>.ValidationError(ex.Message));
    }
}
```

### **7. Current Request Context**
```csharp
[HttpGet("profile")]
public async Task<IActionResult> GetProfile()
{
    try
    {
        var userId = GetCurrentUserId();
        var userName = GetCurrentUserName();
        var userEmail = GetCurrentUserEmail();
        var userRole = GetCurrentUserRoleId();
        var language = GetCurrentLanguage();
        var correlationId = GetCorrelationId();

        var profile = new
        {
            Id = userId,
            Name = userName,
            Email = userEmail,
            RoleId = userRole,
            Language = language,
            CorrelationId = correlationId
        };

        return Ok(ApiResponse<object>.Success(profile, "Profile Retrieved Successfully"));
    }
    catch (Exception ex)
    {
        return BadRequest(ApiResponse<object>.Error("Failed to retrieve profile", ex));
    }
}
```

## Validation Methods

### **1. ValidateModelState**
```csharp
protected void ValidateModelState(string messageCode, string enMessage, string arMessage)
```
- Validates ModelState.IsValid
- Throws BusinessValidationException if invalid
- Provides localized error messages
- Includes all validation errors in message

### **2. ValidateListNotEmpty**
```csharp
protected void ValidateListNotEmpty<T>(IEnumerable<T> list, string messageCode, string enMessage, string arMessage)
```
- Validates that a list is not null or empty
- Throws BusinessValidationException if invalid
- Supports any IEnumerable type

### **3. ValidateStringNotEmpty**
```csharp
protected void ValidateStringNotEmpty(string value, string messageCode, string enMessage, string arMessage)
```
- Validates that a string is not null, empty, or whitespace
- Throws BusinessValidationException if invalid
- Useful for required string parameters

### **4. ValidateNotNull**
```csharp
protected void ValidateNotNull<T>(T value, string messageCode, string enMessage, string arMessage)
```
- Validates that a value is not null
- Throws BusinessValidationException if invalid
- Supports any reference type

### **5. ValidateCondition**
```csharp
protected void ValidateCondition(bool condition, string messageCode, string enMessage, string arMessage)
```
- Validates that a condition is true
- Throws BusinessValidationException if false
- Useful for business rule validation

## Current Request Context

### **1. GetCurrentUserId**
```csharp
protected string GetCurrentUserId()
```
- Returns current user ID from JWT claims
- Returns empty string if not authenticated

### **2. GetCurrentUserName**
```csharp
protected string GetCurrentUserName()
```
- Returns current user name from JWT claims
- Returns empty string if not authenticated

### **3. GetCurrentUserEmail**
```csharp
protected string GetCurrentUserEmail()
```
- Returns current user email from JWT claims
- Returns empty string if not authenticated

### **4. GetCurrentUserRoleId**
```csharp
protected int GetCurrentUserRoleId()
```
- Returns current user role ID from JWT claims
- Returns 0 if not authenticated or invalid role

### **5. GetCurrentLanguage**
```csharp
protected string GetCurrentLanguage()
```
- Returns current language from Accept-Language header
- Defaults to "en" if not specified

### **6. GetCorrelationId**
```csharp
protected string GetCorrelationId()
```
- Returns request correlation ID for tracing
- Uses HttpContext.TraceIdentifier or generates new GUID

## Error Handling

### **1. BusinessValidationException**
```csharp
catch (BusinessValidationException ex)
{
    return BadRequest(ApiResponse<T>.ValidationError(ex.Message));
}
```

### **2. Standard Exception Handling**
```csharp
catch (Exception ex)
{
    return BadRequest(ApiResponse<T>.Error("Operation failed", ex));
}
```

### **3. Specific Exception Handling**
```csharp
catch (ArgumentException ex)
{
    return NotFound(ApiResponse<T>.NotFound(ex.Message));
}
catch (UnauthorizedAccessException ex)
{
    return Unauthorized(ApiResponse<T>.Unauthorized(ex.Message));
}
```

## Localization Support

### **1. Language Detection**
- Reads Accept-Language header
- Supports "en" and "ar" languages
- Defaults to "en" if not specified

### **2. Localized Messages**
```csharp
ValidateModelState("USER_CREATE_VALIDATION_ERROR", 
    "Invalid user data",           // English
    "بيانات المستخدم غير صالحة");   // Arabic
```

### **3. Message Codes**
- Used for error categorization
- Helps with error tracking and monitoring
- Supports internationalization

## Benefits

### **1. Code Reusability**
- ✅ Common validation logic across all controllers
- ✅ Consistent error handling patterns
- ✅ Reduced code duplication

### **2. Localization**
- ✅ Support for English and Arabic
- ✅ Automatic language detection
- ✅ Localized error messages

### **3. Security**
- ✅ Current user context access
- ✅ Permission-based validation
- ✅ Request correlation for tracing

### **4. Maintainability**
- ✅ Centralized validation logic
- ✅ Easy to add new validation methods
- ✅ Consistent error responses

### **5. Developer Experience**
- ✅ Clear validation methods
- ✅ Intuitive API
- ✅ Comprehensive error messages

## Updated Controllers

### **1. AuthController**
- ✅ Inherits from ApiControllerBase
- ✅ Model state validation for login
- ✅ Localized error messages

### **2. SubscriptionPlanController**
- ✅ Inherits from ApiControllerBase
- ✅ Model state validation for create
- ✅ Business validation exception handling

### **3. UserController**
- ✅ Inherits from ApiControllerBase
- ✅ Model state validation for create
- ✅ Current request context access

### **4. AccreditationController**
- ✅ Inherits from ApiControllerBase
- ✅ Model state validation for create
- ✅ Standardized error handling

## Next Steps

### **1. Immediate Improvements**
- [ ] Add more validation methods (email, phone, etc.)
- [ ] Implement role-based validation helpers
- [ ] Add request/response logging
- [ ] Implement audit trail

### **2. Advanced Features**
- [ ] Add validation attributes support
- [ ] Implement custom validation rules
- [ ] Add validation caching
- [ ] Implement validation pipelines

### **3. Monitoring and Analytics**
- [ ] Add validation error tracking
- [ ] Implement performance monitoring
- [ ] Add usage analytics
- [ ] Implement health checks

The base controller implementation is now **complete and production-ready**! 🚀 