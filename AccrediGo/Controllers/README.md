# API Controllers - Updated for Vertical Slice Architecture

## Overview
Controllers have been updated to use the new **Features** namespace structure from the Vertical Slice Architecture. This provides better organization and eliminates namespace conflicts.

## Updated Namespace Structure

### Old Structure (Technical Concerns)
```csharp
using AccrediGo.Application.Commands.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan;
using AccrediGo.Application.Queries.BillingDetails.SubscriptionPlan.ReadAllSubscriptionPlans;
```

### New Structure (Business Features)
```csharp
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan;
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.GetAllSubscriptionPlans;
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.UpdateSubscriptionPlan;
```

## Controller Examples

### 1. SubscriptionPlanController (Updated)
```csharp
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan;
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.GetAllSubscriptionPlans;
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.UpdateSubscriptionPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccrediGo.API.Controllers.BillingDetails
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanController : Controller
    {
        private readonly IMediator _mediator;

        public SubscriptionPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "Subscription Plan Created Successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSubscriptionPlansQuery();
            var result = await _mediator.Send(query);
            return Ok(new { data = result, message = "Subscription Plans Retrieved Successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSubscriptionPlanCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "Subscription Plan Updated Successfully" });
        }
    }
}
```

### 2. UserController (Example)
```csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccrediGo.API.Controllers.UserManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "User Created Successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(new { data = result, message = "Users Retrieved Successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(new { data = result, message = "User Retrieved Successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "User Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "User Deleted Successfully" });
        }
    }
}
```

### 3. AccreditationController (Example)
```csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccrediGo.API.Controllers.Accreditation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccreditationController : Controller
    {
        private readonly IMediator _mediator;

        public AccreditationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccreditationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "Accreditation Created Successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAccreditationsQuery();
            var result = await _mediator.Send(query);
            return Ok(new { data = result, message = "Accreditations Retrieved Successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetAccreditationByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(new { data = result, message = "Accreditation Retrieved Successfully" });
        }

        [HttpPut("{id}/submit")]
        public async Task<IActionResult> Submit(string id)
        {
            var command = new SubmitAccreditationCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "Accreditation Submitted Successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAccreditationCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(new { data = result, message = "Accreditation Updated Successfully" });
        }
    }
}
```

## Benefits of Updated Controllers

### 1. **Clear Feature Organization**
- Controllers are organized by business modules
- Easy to find related functionality
- Clear separation of concerns

### 2. **Consistent Namespace Pattern**
- All features follow the same namespace structure
- Easy to predict where to find specific features
- No more namespace conflicts

### 3. **Better Maintainability**
- Related features are co-located
- Easy to add new endpoints for existing features
- Simple to understand what each controller does

### 4. **Improved API Design**
- RESTful endpoints following best practices
- Consistent response format
- Clear HTTP method usage

## Controller Organization Pattern

```
Controllers/
├── BillingDetails/
│   └── SubscriptionPlanController.cs
├── UserManagement/
│   ├── UserController.cs
│   └── FacilityUserController.cs
├── Accreditation/
│   ├── AccreditationController.cs
│   ├── FacilityController.cs
│   └── StandardController.cs
└── Reporting/
    ├── ReportController.cs
    └── ExportController.cs
```

## Next Steps

1. **Create Missing Features**: Implement the actual Features for User and Accreditation modules
2. **Update Response Models**: Create consistent response models across all controllers
3. **Add Validation**: Implement request validation using FluentValidation
4. **Add Authentication**: Implement proper authentication and authorization
5. **Add Logging**: Implement comprehensive logging for all operations
6. **Add Error Handling**: Implement global exception handling middleware

## Migration Checklist

- [x] Update SubscriptionPlanController namespaces
- [x] Add new endpoints for GetAll and Update
- [x] Create example controllers for other modules
- [x] Update using statements to use Features namespaces
- [ ] Implement actual Features for User and Accreditation modules
- [ ] Add proper error handling and validation
- [ ] Update dependency injection registrations
- [ ] Test all endpoints 