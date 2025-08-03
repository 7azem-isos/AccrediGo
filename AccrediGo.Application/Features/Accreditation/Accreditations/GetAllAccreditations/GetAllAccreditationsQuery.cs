using MediatR;

namespace AccrediGo.Application.Features.Accreditation.Accreditations.GetAllAccreditations
{
    public class GetAllAccreditationsQuery : IRequest<IEnumerable<GetAllAccreditationsDto>>
    {
        // Query parameters can be added here if needed
    }
} 