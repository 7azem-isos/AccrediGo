using Microsoft.AspNetCore.Mvc;
using AccrediGo.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AccrediGo.Application.Services;
using System;

namespace AccrediGo.Controllers
{
    [ApiController]
    [Route("api/auth/resend-verification")]
    public class ResendVerificationController : ControllerBase
    {
        private readonly AccrediGoDbContext _db;
        private readonly IMailService _mailService;
        public ResendVerificationController(AccrediGoDbContext db, IMailService mailService)
        {
            _db = db;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Resend([FromBody] string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return NotFound("User not found.");
            if (user.IsEmailVerified)
                return BadRequest("Email is already verified.");
            // Generate new token
            user.EmailVerificationToken = Guid.NewGuid().ToString("N");
            user.EmailVerificationTokenExpiresAt = DateTime.UtcNow.AddHours(24);
            await _db.SaveChangesAsync();
            // Send email
            var verificationUrl = $"https://yourdomain.com/api/auth/verify-email?token={user.EmailVerificationToken}";
            var emailBody = $"<p>Please verify your email by clicking <a href='{verificationUrl}'>here</a>.</p>";
            await _mailService.SendEmailAsync(user.Email, "Verify your email address", emailBody);
            return Ok("Verification email resent.");
        }
    }
}
