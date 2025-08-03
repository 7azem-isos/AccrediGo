using MediatR;

namespace AccrediGo.Application.Interfaces
{
    /// <summary>
    /// Interface for delete commands that require audit tracking
    /// </summary>
    /// <typeparam name="TResponse">The response type for the command</typeparam>
    public interface IDeleteCommand<TResponse> : IRequest<TResponse>, IAuditableCommand
    {
        /// <summary>
        /// The ID of the user who deleted the record
        /// </summary>
        string DeletedBy { get; set; }

        /// <summary>
        /// The timestamp when the record was deleted
        /// </summary>
        DateTime DeletedAt { get; set; }

        /// <summary>
        /// The IP address of the user who deleted the record
        /// </summary>
        string DeletedFromIp { get; set; }

        /// <summary>
        /// The reason for deletion
        /// </summary>
        string DeletionReason { get; set; }
    }
} 