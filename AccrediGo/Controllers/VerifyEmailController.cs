using Microsoft.AspNetCore.Mvc;
using AccrediGo.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccrediGo.Controllers
{
    [ApiController]
    [Route("api/auth/verify-email")]
    public class VerifyEmailController : ControllerBase
    {
        private readonly AccrediGoDbContext _db;
        public VerifyEmailController(AccrediGoDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Verify([FromQuery] string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token);
            if (user == null || user.EmailVerificationTokenExpiresAt < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired verification token.");
            }
            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpiresAt = null;
            await _db.SaveChangesAsync();
            return Ok("Email verified successfully. You can now log in.");
        }
    }
}
