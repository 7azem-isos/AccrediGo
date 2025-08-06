using System;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.UserManagement.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuditService auditService,
            ILogger<CreateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling CreateUserCommand. User: {UserName}, Email: {Email}", 
                    request.Name, request.Email);

                // Populate audit information
                _auditService.PopulateAuditInfo(request);

                var user = _mapper.Map<User>(request);
                // Hash the password before saving
                user.Password = AccrediGo.Application.Common.PasswordHasher.HashPassword(request.Password ?? string.Empty);
                // Set ID and creation timestamp
                user.Id = Guid.NewGuid().ToString();
                user.CreatedAt = request.CreatedAt;
                
                // Use CancellationToken in repository operations
                var createdUser = await _unitOfWork.GetRepository<User>().AddAsync(user, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                // _logger.LogInformation("Successfully created user with ID: {UserId}. CreatedBy: {CreatedBy}, CreatedFromIp: {CreatedFromIp}", 
                //     createdUser.Id, request.CreatedBy, request.CreatedFromIp);
                
                return _mapper.Map<CreateUserDto>(createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user. User: {UserName}, Email: {Email}", 
                    request.Name, request.Email);
                throw;
            }
        }
    }
} 