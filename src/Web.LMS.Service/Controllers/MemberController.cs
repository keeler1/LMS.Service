using Infrastructure.LMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Web.LMS.Service.dto;

namespace Web.LMS.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MemberController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<MemberDto>>> Get()
        {
            var members = await _context.Members
                .Take(5)
                .OrderBy(m => m.LastName)
                .ToListAsync();

            var dto = members.Select(m => new MemberDto
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber
            }).ToList();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MemberDto>>> Get( Guid id)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null) return NotFound();

            var dto = new MemberDto
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(MemberCreatedDto dto)
        {
            var member = new Member
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = member.Id }, member.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, MemberCreatedDto dto)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null) return NotFound();

            member.FirstName = dto.FirstName;
            member.LastName = dto.LastName;
            member.Email = dto.Email;
            member.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null) return NotFound();

            _context.Members.Remove(member);
            _context.SaveChanges();
            return NoContent();
        }
    }
}