using System;
using System.Threading;
using System.Threading.Tasks;
using AccrediGo.Domain.Entities.UserDetails;
using AccrediGo.Domain.Entities.MainComponents;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccrediGo.Application.Features.UserManagement.FacilityUsers.CreateFacilityUser
{
    public class CreateFacilityUserCommandHandler : IRequestHandler<CreateFacilityUserCommand, CreateFacilityUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly ILogger<CreateFacilityUserCommandHandler> _logger;

        public CreateFacilityUserCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuditService auditService,
            ILogger<CreateFacilityUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateFacilityUserDto> Handle(CreateFacilityUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateFacilityUserCommand. Staff: {Name}, Email: {Email}", request.Name, request.Email);
            _auditService.PopulateAuditInfo(request);

            // Create User
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                ArabicName = request.ArabicName,
                Email = request.Email,
                PhoneNumber = request.Phone,
                SystemRoleId = request.SystemRoleId,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy,
                UserAgent = request.UserAgent,
                CreatedFromIp = request.CreatedFromIp
            };
            await _unitOfWork.GetRepository<User>().AddAsync(user, cancellationToken);

            // Create FacilityUser
            var facilityUser = new FacilityUser
            {
                UserID = user.Id,
                FacilityUserId = request.FacilityId,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy,
                UserAgent = request.UserAgent,
                CreatedFromIp = request.CreatedFromIp
            };
            await _unitOfWork.GetRepository<FacilityUser>().AddAsync(facilityUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully created staff member with UserId: {UserId}", user.Id);
            return _mapper.Map<CreateFacilityUserDto>(user);
        }
    }
}
