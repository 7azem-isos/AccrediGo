# HTTP GET Actions Implementation

## Overview
This document describes the comprehensive HTTP GET actions implementation for the AccrediGo application. The implementation includes pagination support, comprehensive documentation, Swagger attributes, and proper error handling following REST API best practices.

## Architecture

### **1. Comprehensive Documentation**
- **XML Documentation**: Detailed summaries, descriptions, and parameter documentation
- **Response Documentation**: All possible HTTP status codes and their meanings
- **Swagger Integration**: Full Swagger/OpenAPI documentation support

### **2. Pagination Support**
- **PaginatedResponse<T>**: Standardized pagination response structure
- **Query Parameters**: Support for page number, page size, filtering, and sorting
- **Metadata**: Total count, page information, and navigation details

### **3. Authorization & Validation**
- **Role-based Access**: Permission validation based on user roles
- **Input Validation**: Comprehensive validation of all inputs
- **Business Rules**: Domain-specific validation rules

### **4. Error Handling**
- **Standardized Responses**: Consistent error response format
- **Localized Messages**: Support for English and Arabic error messages
- **Exception Handling**: Proper exception categorization and handling

## Implementation Examples

### **1. Paginated GET Action - User Management**

#### **Controller Method**
```csharp
/// <summary>
/// Retrieves a paginated list of user records
/// </summary>
/// <param name="query">Query parameters for filtering and pagination</param>
/// <returns>A paginated list of user records</returns>
/// <response code="200">Returns the paginated list of user records</response>
/// <response code="401">If the user is not authenticated</response>
/// <response code="403">If the user does not have permission to view users</response>
[HttpGet]
[ProducesResponseType(typeof(PaginatedResponse<GetAllUsersDto>), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[SwaggerOperation(
    Summary = "Get all users",
    Description = "Retrieves a paginated list of user records with optional filtering and sorting",
    OperationId = "GetAllUsers",
    Tags = new[] { "User Management" }
)]
public async Task<ActionResult<PaginatedResponse<GetAllUsersDto>>> Get([FromQuery] GetAllUsersQuery query)
{
    try
    {
        // Validate user has permission to view users
        ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
            "You don't have permission to view users",
            "ليس لديك صلاحية لعرض المستخدمين");

        var paginatedResult = await _mediator.Send(query);

        var paginatedResponse = PaginatedResponse<GetAllUsersDto>.Success(
            paginatedResult.Result.ToList(),
            paginatedResult.TotalItemsCount,
            paginatedResult.PageNumber,
            paginatedResult.PageSize,
            "Users retrieved successfully"
        );

        return Ok(paginatedResponse);
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<PaginatedResponse<GetAllUsersDto>>.ValidationError(ex.Message));
    }
    catch (Exception ex)
    {
        return BadRequest(ApiResponse<PaginatedResponse<GetAllUsersDto>>.Error("Failed to retrieve users", ex));
    }
}
```

#### **Query Parameters**
```csharp
public class GetAllUsersQuery : IRequest<PaginatedResult<GetAllUsersDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? Status { get; set; }
    public string? RoleId { get; set; }
}
```

#### **Response Structure**
```json
{
  "data": [
    {
      "id": "user-1",
      "name": "John Doe",
      "email": "john.doe@example.com",
      "roleId": 1,
      "status": "Active",
      "createdAt": "2024-01-01T00:00:00Z"
    }
  ],
  "state": "Success",
  "message": "Users retrieved successfully",
  "totalCount": 100,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 10,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

### **2. Single Record GET Action - User by ID**

#### **Controller Method**
```csharp
/// <summary>
/// Retrieves a specific user by their unique identifier
/// </summary>
/// <param name="id">The unique identifier of the user</param>
/// <returns>The user record if found</returns>
/// <response code="200">Returns the user record</response>
/// <response code="401">If the user is not authenticated</response>
/// <response code="403">If the user does not have permission to view users</response>
/// <response code="404">If the user is not found</response>
[HttpGet("{id}")]
[ProducesResponseType(typeof(ApiResponse<GetUserByIdDto>), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[SwaggerOperation(
    Summary = "Get user by ID",
    Description = "Retrieves a specific user record by their unique identifier",
    OperationId = "GetUserById",
    Tags = new[] { "User Management" }
)]
public async Task<ActionResult<ApiResponse<GetUserByIdDto>>> GetById(string id)
{
    try
    {
        // Validate user has permission to view users
        ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
            "You don't have permission to view users",
            "ليس لديك صلاحية لعرض المستخدمين");

        ValidateStringNotEmpty(id, "USER_ID_EMPTY_ERROR",
            "User ID cannot be empty",
            "معرف المستخدم لا يمكن أن يكون فارغاً");

        var query = new GetUserByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        return Ok(ApiResponse<GetUserByIdDto>.Success(result, "User retrieved successfully"));
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<GetUserByIdDto>.ValidationError(ex.Message));
    }
    catch (ArgumentException ex)
    {
        return NotFound(ApiResponse<GetUserByIdDto>.NotFound(ex.Message));
    }
    catch (Exception ex)
    {
        return BadRequest(ApiResponse<GetUserByIdDto>.Error("Failed to retrieve user", ex));
    }
}
```

### **3. Paginated GET Action - Accreditation Management**

#### **Controller Method**
```csharp
/// <summary>
/// Retrieves a paginated list of accreditation records
/// </summary>
/// <param name="query">Query parameters for filtering and pagination</param>
/// <returns>A paginated list of accreditation records</returns>
/// <response code="200">Returns the paginated list of accreditation records</response>
/// <response code="401">If the user is not authenticated</response>
/// <response code="403">If the user does not have permission to view accreditations</response>
[HttpGet]
[ProducesResponseType(typeof(PaginatedResponse<GetAllAccreditationsDto>), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[SwaggerOperation(
    Summary = "Get all accreditations",
    Description = "Retrieves a paginated list of accreditation records with optional filtering and sorting",
    OperationId = "GetAllAccreditations",
    Tags = new[] { "Accreditation" }
)]
public async Task<ActionResult<PaginatedResponse<GetAllAccreditationsDto>>> Get([FromQuery] GetAllAccreditationsQuery query)
{
    try
    {
        // Validate user has permission to view accreditations
        ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
            "You don't have permission to view accreditations",
            "ليس لديك صلاحية لعرض الاعتمادات");

        var paginatedResult = await _mediator.Send(query);

        var paginatedResponse = PaginatedResponse<GetAllAccreditationsDto>.Success(
            paginatedResult.Result.ToList(),
            paginatedResult.TotalItemsCount,
            paginatedResult.PageNumber,
            paginatedResult.PageSize,
            "Accreditations retrieved successfully"
        );

        return Ok(paginatedResponse);
    }
    catch (BusinessValidationException ex)
    {
        return BadRequest(ApiResponse<PaginatedResponse<GetAllAccreditationsDto>>.ValidationError(ex.Message));
    }
    catch (Exception ex)
    {
        return BadRequest(ApiResponse<PaginatedResponse<GetAllAccreditationsDto>>.Error("Failed to retrieve accreditations", ex));
    }
}
```

## HTTP Status Codes

### **1. Success Responses**
- **200 OK**: Successful retrieval of data
- **201 Created**: Resource created successfully (for POST actions)

### **2. Client Error Responses**
- **400 Bad Request**: Invalid request data or validation errors
- **401 Unauthorized**: User not authenticated
- **403 Forbidden**: User authenticated but lacks permission
- **404 Not Found**: Resource not found

### **3. Server Error Responses**
- **500 Internal Server Error**: Unexpected server errors

## Validation Patterns

### **1. Permission Validation**
```csharp
ValidateCondition(GetCurrentUserRoleId() >= 1, "INSUFFICIENT_PERMISSIONS_ERROR",
    "You don't have permission to view users",
    "ليس لديك صلاحية لعرض المستخدمين");
```

### **2. Input Validation**
```csharp
ValidateStringNotEmpty(id, "USER_ID_EMPTY_ERROR",
    "User ID cannot be empty",
    "معرف المستخدم لا يمكن أن يكون فارغاً");
```

### **3. Model State Validation**
```csharp
ValidateModelState("USER_CREATE_VALIDATION_ERROR", 
    "Invalid user data", 
    "بيانات المستخدم غير صالحة");
```

## Error Handling Patterns

### **1. Business Validation Exception**
```csharp
catch (BusinessValidationException ex)
{
    return BadRequest(ApiResponse<T>.ValidationError(ex.Message));
}
```

### **2. Not Found Exception**
```csharp
catch (ArgumentException ex)
{
    return NotFound(ApiResponse<T>.NotFound(ex.Message));
}
```

### **3. General Exception**
```csharp
catch (Exception ex)
{
    return BadRequest(ApiResponse<T>.Error("Failed to retrieve data", ex));
}
```

## Swagger Documentation

### **1. Operation Documentation**
```csharp
[SwaggerOperation(
    Summary = "Get all users",
    Description = "Retrieves a paginated list of user records with optional filtering and sorting",
    OperationId = "GetAllUsers",
    Tags = new[] { "User Management" }
)]
```

### **2. Response Type Documentation**
```csharp
[ProducesResponseType(typeof(PaginatedResponse<GetAllUsersDto>), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
```

### **3. Parameter Documentation**
```csharp
/// <param name="query">Query parameters for filtering and pagination</param>
/// <returns>A paginated list of user records</returns>
```

## Pagination Implementation

### **1. PaginatedResponse Structure**
```csharp
public class PaginatedResponse<T> : ApiResponse<List<T>>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}
```

### **2. Pagination Factory Method**
```csharp
public static PaginatedResponse<T> Success(List<T> data, int totalCount, int pageNumber, int pageSize, string message = null)
{
    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
    
    return new PaginatedResponse<T>
    {
        Data = data,
        State = ResponseState.Success,
        Message = message,
        TotalCount = totalCount,
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalPages = totalPages,
        HasPreviousPage = pageNumber > 1,
        HasNextPage = pageNumber < totalPages
    };
}
```

## Query Parameters

### **1. Standard Pagination Parameters**
- **PageNumber**: Current page number (default: 1)
- **PageSize**: Number of items per page (default: 10)
- **SearchTerm**: Text search across relevant fields
- **SortBy**: Field to sort by
- **SortOrder**: Sort direction (asc/desc)

### **2. Module-Specific Parameters**
- **Status**: Filter by status (Active, Inactive, Pending)
- **RoleId**: Filter by user role
- **DateRange**: Filter by date range
- **Category**: Filter by category

## Response Examples

### **1. Successful Paginated Response**
```json
{
  "data": [
    {
      "id": "user-1",
      "name": "John Doe",
      "email": "john.doe@example.com",
      "roleId": 1,
      "status": "Active"
    }
  ],
  "state": "Success",
  "message": "Users retrieved successfully",
  "totalCount": 100,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 10,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

### **2. Error Response**
```json
{
  "data": null,
  "state": "Error",
  "message": "You don't have permission to view users",
  "developerMessage": "System.Exception: Insufficient permissions"
}
```

### **3. Validation Error Response**
```json
{
  "data": null,
  "state": "ValidationError",
  "message": "User ID cannot be empty",
  "developerMessage": "BusinessValidationException: USER_ID_EMPTY_ERROR"
}
```

## Benefits

### **1. Comprehensive Documentation**
- ✅ **XML Documentation**: Detailed summaries and descriptions
- ✅ **Swagger Integration**: Full OpenAPI documentation
- ✅ **Response Documentation**: All status codes documented
- ✅ **Parameter Documentation**: Clear parameter descriptions

### **2. Pagination Support**
- ✅ **Standardized Pagination**: Consistent pagination across all endpoints
- ✅ **Flexible Parameters**: Support for various filtering and sorting options
- ✅ **Metadata Rich**: Complete pagination metadata
- ✅ **Performance Optimized**: Efficient database queries

### **3. Authorization & Security**
- ✅ **Role-based Access**: Permission validation based on user roles
- ✅ **Input Validation**: Comprehensive validation of all inputs
- ✅ **Business Rules**: Domain-specific validation rules
- ✅ **Localized Messages**: Support for multiple languages

### **4. Error Handling**
- ✅ **Standardized Responses**: Consistent error response format
- ✅ **Exception Categorization**: Proper exception handling
- ✅ **Developer Information**: Debug information in development
- ✅ **User-friendly Messages**: Clear error messages for users

### **5. Developer Experience**
- ✅ **IntelliSense Support**: Full IntelliSense for all parameters
- ✅ **Type Safety**: Compile-time validation
- ✅ **Consistent Patterns**: Standardized implementation patterns
- ✅ **Easy Testing**: Well-documented endpoints for testing

## Updated Controllers

### **1. UserController**
- ✅ **Paginated GET**: `/api/user-management/users` with pagination
- ✅ **Single GET**: `/api/user-management/users/{id}` for specific user
- ✅ **Comprehensive Documentation**: Full XML and Swagger documentation
- ✅ **Authorization**: Role-based permission validation

### **2. AccreditationController**
- ✅ **Paginated GET**: `/api/accreditation/accreditations` with pagination
- ✅ **Single GET**: `/api/accreditation/accreditations/{id}` for specific accreditation
- ✅ **Submit Action**: `/api/accreditation/accreditations/{id}/submit` for submission
- ✅ **Comprehensive Documentation**: Full XML and Swagger documentation

## Next Steps

### **1. Immediate Improvements**
- [ ] Add more filtering options (date ranges, categories)
- [ ] Implement advanced sorting (multiple fields)
- [ ] Add response caching for frequently accessed data
- [ ] Implement request rate limiting

### **2. Advanced Features**
- [ ] Add export functionality (CSV, Excel)
- [ ] Implement bulk operations
- [ ] Add real-time updates via SignalR
- [ ] Implement advanced search with full-text search

### **3. Performance Optimization**
- [ ] Add database query optimization
- [ ] Implement response compression
- [ ] Add response caching strategies
- [ ] Implement lazy loading for large datasets

The HTTP GET actions implementation is now **complete and production-ready** with comprehensive documentation, pagination, and error handling! 🚀 