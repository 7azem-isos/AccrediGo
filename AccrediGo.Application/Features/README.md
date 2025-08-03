# Vertical Slice Architecture - Features Folder

## Overview
This project uses **Vertical Slice Architecture** (also known as Feature Folders) to organize CQRS and Mediator pattern implementations. This approach groups all related code for a specific business feature together, rather than organizing by technical concerns.

## Structure

```
Features/
├── [Module]/
│   ├── [Entity]/
│   │   ├── [Action]/
│   │   │   ├── [Action][Entity]Command.cs
│   │   │   ├── [Action][Entity]CommandHandler.cs
│   │   │   ├── [Action][Entity]Dto.cs
│   │   │   └── [Action][Entity]Validator.cs (optional)
│   │   └── [AnotherAction]/
│   └── [AnotherEntity]/
└── [AnotherModule]/
```

## Benefits

### 1. **Solves Namespace Conflicts**
- Each feature is self-contained
- No more `CreateSubscriptionPlanCommand` vs `CreateUserCommand` conflicts
- Clear feature boundaries

### 2. **Improved Maintainability**
- All related code is co-located
- Easy to understand what a feature does
- Simple to add/remove entire features

### 3. **Better Team Collaboration**
- Teams can work on different features independently
- Clear ownership of features
- Reduced merge conflicts

### 4. **Enhanced Discoverability**
- Easy to find all code related to a specific feature
- Clear business domain organization
- Intuitive for new developers

## Examples

### BillingDetails Module
```
Features/BillingDetails/
├── SubscriptionPlans/
│   ├── CreateSubscriptionPlan/
│   │   ├── CreateSubscriptionPlanCommand.cs
│   │   ├── CreateSubscriptionPlanCommandHandler.cs
│   │   └── CreateSubscriptionPlanDto.cs
│   ├── UpdateSubscriptionPlan/
│   │   ├── UpdateSubscriptionPlanCommand.cs
│   │   ├── UpdateSubscriptionPlanCommandHandler.cs
│   │   └── UpdateSubscriptionPlanDto.cs
│   ├── DeleteSubscriptionPlan/
│   │   ├── DeleteSubscriptionPlanCommand.cs
│   │   └── DeleteSubscriptionPlanCommandHandler.cs
│   └── GetAllSubscriptionPlans/
│       ├── GetAllSubscriptionPlansQuery.cs
│       ├── GetAllSubscriptionPlansQueryHandler.cs
│       └── GetAllSubscriptionPlansDto.cs
├── Payments/
│   ├── CreatePayment/
│   ├── ProcessPayment/
│   └── GetPaymentHistory/
└── Features/
    ├── CreateFeature/
    └── AssignFeatureToPlan/
```

### UserManagement Module
```
Features/UserManagement/
├── Users/
│   ├── CreateUser/
│   ├── UpdateUser/
│   ├── DeactivateUser/
│   └── GetUserById/
└── FacilityUsers/
    ├── AssignUserToFacility/
    ├── RemoveUserFromFacility/
    └── GetFacilityUsers/
```

## Naming Conventions

### Module Names (PascalCase)
- `BillingDetails`
- `UserManagement`
- `Accreditation`
- `FacilityManagement`
- `Reporting`

### Entity Names (PascalCase)
- `SubscriptionPlan`
- `User`
- `Facility`
- `Accreditation`
- `Payment`

### Action Names (PascalCase)
- `Create`
- `Update`
- `Delete`
- `GetAll`
- `GetById`
- `Activate`
- `Deactivate`

## Migration from Old Structure

### Old Structure (Technical Concerns)
```
Commands/
├── BillingDetails/
│   └── SubscriptionPlans/
│       └── CreateSubscriptionPlan/
Queries/
├── BillingDetails/
│   └── SubscriptionPlan/
│       └── ReadAllSubscriptionPlans/
```

### New Structure (Business Features)
```
Features/
├── BillingDetails/
│   └── SubscriptionPlans/
│       ├── CreateSubscriptionPlan/
│       ├── UpdateSubscriptionPlan/
│       ├── DeleteSubscriptionPlan/
│       └── GetAllSubscriptionPlans/
```

## Best Practices

1. **Keep Features Self-Contained**: All related files for a feature should be in the same folder
2. **Use Consistent Naming**: Follow the established naming conventions
3. **Group by Business Domain**: Organize by business modules, not technical concerns
4. **Include Related Files**: Commands, Queries, DTOs, Validators, etc. all in the same folder
5. **Use Clear Action Names**: Make the action obvious from the folder name

## Migration Steps

1. Create the new `Features` folder structure
2. Move existing Commands and Queries to appropriate feature folders
3. Update namespaces in all moved files
4. Update any references to the old structure
5. Remove the old Commands and Queries folders
6. Update dependency injection registrations if needed 