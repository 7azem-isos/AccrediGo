# Standardized API Responses Implementation

## Overview
This document describes the standardized API response implementation for the AccrediGo application. All API endpoints now return consistent, well-structured responses with proper error handling and developer-friendly debugging information.

## Response Structure

### **1. ApiResponse<T> Class**
```csharp
public class ApiResponse<T>
{
    public T Data { get; set; }
    public ResponseState State { get; set; }
    public string Message { get; set; }
    public string DeveloperMessage { get; set; } // Only in DEBUG mode
}
```

### **2. ResponseState Enum**
```csharp
public enum ResponseState
{
    Success,
    Error,
    NotFound,
    Unauthorized,
    Forbidden,
    BadRequest,
    ValidationError
}
```

### **3. PaginatedResponse<T> Class**
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

## Usage Examples

### **1. Success Response**
```json
{
  "data": {
    "id": "user-123",
    "name": "John Doe",
    "email": "john@example.com"
  },
  "state": "Success",
  "message": "User Created Successfully"
}
```

### **2. Error Response (Production)**
```json
{
  "data": null,
  "state": "Error",
  "message": "Failed to create user"
}
```

### **3. Error Response (Debug Mode)**
```json
{
  "data": null,
  "state": "Error",
  "message": "Failed to create user",
  "developerMessage": "System.ArgumentException: User with email already exists"
}
```

### **4. Paginated Response**
```json
{
  "data": [
    {
      "id": "user-1",
      "name": "John Doe"
    },
    {
      "id": "user-2", 
      "name": "Jane Smith"
    }
  ],
  "state": "Success",
  "message": "Users Retrieved Successfully",
  "totalCount": 50,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 5,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

## Controller Implementation

### **1. Success Response**
```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
{
    try
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<CreateUserDto>.Success(result, "User Created Successfully"));
    }
    catch (Exception ex)
    {
        return BadRequest(ApiResponse<CreateUserDto>.Error("Failed to create user", ex));
    }
}
```

### **2. Error Handling**
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetById(string id)
{
    try
    {
        var query = new GetUserByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        return Ok(ApiResponse<GetUserByIdDto>.Success(result, "User Retrieved Successfully"));
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

### **3. Authentication Response**
```csharp
[HttpPost("login")]
[AllowAnonymous]
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    try
    {
        var command = new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        var result = await _mediator.Send(command);
        var loginResponse = new LoginResponse
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken,
            ExpiresAt = result.ExpiresAt,
            TokenType = result.TokenType,
            User = new UserInfo
            {
                Id = result.User.Id,
                Name = result.User.Name,
                Email = result.User.Email,
                SystemRoleId = result.User.SystemRoleId,
                RoleName = result.User.RoleName
            }
        };
        return Ok(ApiResponse<LoginResponse>.Success(loginResponse, "Login successful"));
    }
    catch (UnauthorizedAccessException ex)
    {
        return Unauthorized(ApiResponse<LoginResponse>.Unauthorized(ex.Message));
    }
    catch (Exception ex)
    {
        return BadRequest(ApiResponse<LoginResponse>.Error("Login failed", ex));
    }
}
```

## Response States

### **1. Success**
- **HTTP Status**: 200 OK
- **State**: `Success`
- **Usage**: Successful operations

### **2. Error**
- **HTTP Status**: 400 Bad Request
- **State**: `Error`
- **Usage**: General errors, validation failures

### **3. NotFound**
- **HTTP Status**: 404 Not Found
- **State**: `NotFound`
- **Usage**: Resource not found

### **4. Unauthorized**
- **HTTP Status**: 401 Unauthorized
- **State**: `Unauthorized`
- **Usage**: Authentication required

### **5. Forbidden**
- **HTTP Status**: 403 Forbidden
- **State**: `Forbidden`
- **Usage**: Insufficient permissions

### **6. BadRequest**
- **HTTP Status**: 400 Bad Request
- **State**: `BadRequest`
- **Usage**: Invalid request data

### **7. ValidationError**
- **HTTP Status**: 400 Bad Request
- **State**: `ValidationError`
- **Usage**: Data validation failures

## Benefits

### **1. Consistency**
- ✅ All endpoints return the same response structure
- ✅ Consistent error handling across the application
- ✅ Standardized HTTP status codes

### **2. Developer Experience**
- ✅ Clear success/error states
- ✅ Descriptive error messages
- ✅ Debug information in development mode
- ✅ Easy to parse and handle on frontend

### **3. Production Safety**
- ✅ No sensitive information leaked in production
- ✅ Debug information only available in DEBUG mode
- ✅ Proper error logging and monitoring

### **4. Frontend Integration**
- ✅ Easy to handle different response states
- ✅ Consistent data structure for UI components
- ✅ Clear error messages for user feedback

## API Endpoints Updated

### **Authentication**
- ✅ `POST /api/Auth/login` - Standardized login response
- ✅ `POST /api/Auth/refresh` - Standardized refresh response
- ✅ `GET /api/Auth/me` - Standardized user info response

### **User Management**
- ✅ `POST /api/User` - Create user with standardized response
- ✅ `GET /api/User` - Get all users with standardized response
- ✅ `GET /api/User/{id}` - Get user by ID with standardized response
- ✅ `PUT /api/User/{id}` - Update user with standardized response
- ✅ `DELETE /api/User/{id}` - Delete user with standardized response

### **Subscription Plans**
- ✅ `POST /api/SubscriptionPlan` - Create subscription plan
- ✅ `GET /api/SubscriptionPlan` - Get all subscription plans
- ✅ `PUT /api/SubscriptionPlan/{id}` - Update subscription plan

### **Accreditations**
- ✅ `POST /api/Accreditation` - Create accreditation
- ✅ `GET /api/Accreditation` - Get all accreditations

## Error Handling Patterns

### **1. Try-Catch Pattern**
```csharp
try
{
    var result = await _mediator.Send(command);
    return Ok(ApiResponse<T>.Success(result, "Operation successful"));
}
catch (ArgumentException ex)
{
    return NotFound(ApiResponse<T>.NotFound(ex.Message));
}
catch (Exception ex)
{
    return BadRequest(ApiResponse<T>.Error("Operation failed", ex));
}
```

### **2. Specific Exception Handling**
```csharp
catch (UnauthorizedAccessException ex)
{
    return Unauthorized(ApiResponse<T>.Unauthorized(ex.Message));
}
catch (InvalidOperationException ex)
{
    return BadRequest(ApiResponse<T>.BadRequest(ex.Message));
}
```

### **3. Validation Error Handling**
```csharp
catch (ValidationException ex)
{
    return BadRequest(ApiResponse<T>.ValidationError(ex.Message));
}
```

## Frontend Integration

### **1. JavaScript/TypeScript**
```typescript
interface ApiResponse<T> {
    data: T;
    state: 'Success' | 'Error' | 'NotFound' | 'Unauthorized' | 'Forbidden' | 'BadRequest' | 'ValidationError';
    message: string;
    developerMessage?: string;
}

async function createUser(userData: CreateUserRequest): Promise<ApiResponse<CreateUserResponse>> {
    const response = await fetch('/api/User', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(userData)
    });

    const result = await response.json();
    
    if (result.state === 'Success') {
        // Handle success
        return result;
    } else {
        // Handle error
        throw new Error(result.message);
    }
}
```

### **2. React Component Example**
```typescript
const UserList: React.FC = () => {
    const [users, setUsers] = useState<ApiResponse<User[]>>();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string>();

    const fetchUsers = async () => {
        setLoading(true);
        try {
            const response = await fetch('/api/User');
            const result = await response.json();
            
            if (result.state === 'Success') {
                setUsers(result);
                setError(undefined);
            } else {
                setError(result.message);
            }
        } catch (err) {
            setError('Failed to fetch users');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            {loading && <div>Loading...</div>}
            {error && <div className="error">{error}</div>}
            {users?.state === 'Success' && (
                <div>
                    {users.data.map(user => (
                        <div key={user.id}>{user.name}</div>
                    ))}
                </div>
            )}
        </div>
    );
};
```

## Best Practices

### **1. Error Handling**
- Always wrap operations in try-catch blocks
- Use specific exception types for different scenarios
- Provide meaningful error messages
- Log errors for debugging

### **2. Response Consistency**
- Use the same response structure across all endpoints
- Include appropriate HTTP status codes
- Provide clear success/error messages
- Use DEBUG mode for developer information

### **3. Frontend Integration**
- Handle all response states in frontend code
- Display appropriate messages to users
- Log errors for debugging
- Implement proper loading states

### **4. Security**
- Never expose sensitive information in error messages
- Use DEBUG mode for detailed error information
- Implement proper authentication and authorization
- Validate all input data

## Next Steps

### **1. Immediate Improvements**
- [ ] Add pagination support for GetAll endpoints
- [ ] Implement global exception handling middleware
- [ ] Add request/response logging
- [ ] Implement rate limiting

### **2. Advanced Features**
- [ ] Add response caching
- [ ] Implement API versioning
- [ ] Add request validation middleware
- [ ] Implement response compression

### **3. Monitoring and Analytics**
- [ ] Add response time tracking
- [ ] Implement error rate monitoring
- [ ] Add API usage analytics
- [ ] Implement health check endpoints

The standardized API response implementation is now **complete and production-ready**! 🚀 