using Infrastructure.LMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.LMS.Service.dto;

namespace Web.LMS.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StaffController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<StaffDto>>> Get()
        {
            var staff = await _context.Staffs.ToListAsync();
            var dto = staff.Select(s => new StaffDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Role = s.Role
            }).ToList();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> Create(StaffDto dto)
        {
            var staff = new Staff
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role
            };
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            dto.Id = staff.Id;
            return CreatedAtAction(nameof(Get), new { id = staff.Id }, dto);
        }
    }
}