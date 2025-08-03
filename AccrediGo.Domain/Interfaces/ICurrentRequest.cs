namespace AccrediGo.Domain.Interfaces
{
    public interface ICurrentRequest
    {
        string Lang { get; }
        string UserId { get; }
        string UserName { get; }
        string UserEmail { get; }
        int UserRoleId { get; }
        string CompanyId { get; }
        string CorrelationId { get; }
        DateTime RequestTime { get; }
    }
} 