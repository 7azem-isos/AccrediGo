using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Domain.Entities.MainComponents;

namespace AccrediGo.API.Controllers.UserManagement
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class FacilityApprovalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccrediGo.Application.Services.IMailService _mailService;

        public FacilityApprovalController(IUnitOfWork unitOfWork, AccrediGo.Application.Services.IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        // GET: api/facilityapproval/pending
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingFacilities()
        {
            var facilities = await _unitOfWork.GetRepository<Facility>().GetAllAsync();
            var pending = facilities.Where(f => !f.IsApproved).ToList();
            return Ok(pending);
        }

        // POST: api/facilityapproval/{id}/approve
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveFacility(string id)
        {
            var facility = (await _unitOfWork.GetRepository<Facility>().GetAllAsync()).FirstOrDefault(f => f.UserId == id);
            if (facility == null)
                return NotFound();
            facility.IsApproved = true;
            facility.ApprovedAt = DateTime.UtcNow;
            facility.ApprovedBy = User.Identity?.Name;
            await _unitOfWork.SaveChangesAsync();
            // Send notification email
            if (!string.IsNullOrEmpty(facility.Email))
            {
                var subject = "Your facility account has been approved";
                var body = "<p>Your facility account is now active. You may log in.</p>";
                await _mailService.SendEmailAsync(facility.Email, subject, body);
            }
            return Ok();
        }
    }
}
