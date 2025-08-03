namespace AccrediGo.Models.Common
{
    public interface ICurrentRequest
    {
        string Lang { get; }
        string UserId { get; }
        string UserName { get; }
        string UserEmail { get; }
        int UserRoleId { get; }
        string CorrelationId { get; }
        DateTime RequestTime { get; }
    }
} 