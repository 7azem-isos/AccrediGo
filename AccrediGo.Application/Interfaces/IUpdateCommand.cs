using MediatR;
using AccrediGo.Domain.Interfaces;
namespace AccrediGo.Application.Interfaces
{
    /// <summary>
    /// Interface for update commands that require audit tracking
    /// </summary>
    /// <typeparam name="TResponse">The response type for the command</typeparam>
    public interface IUpdateCommand<TResponse> : IRequest<TResponse>, IAuditableCommand
    {
        /// <summary>
        /// The ID of the user who last modified the record
        /// </summary>
        string ModifiedBy { get; set; }

        /// <summary>
        /// The timestamp when the record was last modified
        /// </summary>
        DateTime ModifiedAt { get; set; }

        /// <summary>
        /// The IP address of the user who modified the record
        /// </summary>
        string ModifiedFromIp { get; set; }
    }
} 