namespace AccrediGo.Application.Services
{
    public interface IHttpContextInfo
    {
        string? GetUserIpAddress();
        string? GetUserAgent();
    }
}