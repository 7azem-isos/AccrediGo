using MediatR;

namespace AccrediGo.Application.Interfaces
{
    /// <summary>
    /// Interface for create commands that require audit tracking
    /// </summary>
    /// <typeparam name="TResponse">The response type for the command</typeparam>
    public interface ICreateCommand<TResponse> : IRequest<TResponse>, IAuditableCommand
    {
    }
} 