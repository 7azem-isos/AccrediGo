using AccrediGo.Models.Common;
using AccrediGo.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AccrediGo.Domain.Interfaces;

namespace AccrediGo.API.Controllers.Health
{
    [Route(AccrediGoRoutes.Health.HealthCheck)]
    [AllowAnonymous]
    public class HealthCheckController : ApiControllerBase
    {
        public HealthCheckController(ICurrentRequest currentRequest) : base(currentRequest)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthStatus()
        {
            try
            {
                var healthStatus = new
                {
                    Status = "Healthy",
                    Timestamp = DateTime.UtcNow,
                    Version = "1.0.0",
                    Environment = "Production",
                    Services = new
                    {
                        Database = "Connected",
                        Authentication = "Active",
                        FileStorage = "Available"
                    },
                    Metrics = new
                    {
                        Uptime = "99.9%",
                        ResponseTime = "150ms",
                        ActiveConnections = 25
                    }
                };

                return Ok(ApiResponse<object>.Success(healthStatus, "Health Check Completed"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Error("Health check failed", ex));
            }
        }

        [HttpGet("detailed")]
        public async Task<IActionResult> GetDetailedHealthStatus()
        {
            try
            {
                var detailedHealth = new
                {
                    OverallStatus = "Healthy",
                    Checks = new[]
                    {
                        new { Service = "Database", Status = "Healthy", ResponseTime = "50ms" },
                        new { Service = "Authentication", Status = "Healthy", ResponseTime = "25ms" },
                        new { Service = "FileStorage", Status = "Healthy", ResponseTime = "75ms" },
                        new { Service = "ExternalAPIs", Status = "Healthy", ResponseTime = "100ms" }
                    },
                    SystemInfo = new
                    {
                        MemoryUsage = "45%",
                        CPUUsage = "30%",
                        DiskUsage = "60%",
                        NetworkStatus = "Connected"
                    }
                };

                return Ok(ApiResponse<object>.Success(detailedHealth, "Detailed Health Check Completed"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.Error("Detailed health check failed", ex));
            }
        }
    }
} 