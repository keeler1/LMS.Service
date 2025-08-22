using Infrastructure.LMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.LMS.Service.dto;

namespace Web.LMS.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookCopyController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BookCopyController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<BookCopyDto>>> Get()
        {
            var copies = await _context.BookCopies
                .Include(c => c.Book)
                .Take(5)
                .OrderBy(b => b.CopyNumber)
                .ToListAsync();
            var dto = copies.Select(c => new BookCopyDto
            {
                Id = c.Id,
                CopyNumber = c.CopyNumber,
                IsAvailable = c.IsAvailable
            }).ToList();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<BookCopyDto>>> Get(Guid id)
        {
            var copy = await _context.BookCopies
                .Include(c => c.Book)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (copy == null) return NotFound();
            var dto = new BookCopyDto
            {
                Id = copy.Id,
                CopyNumber = copy.CopyNumber,
                IsAvailable = copy.IsAvailable
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(BookCopyCreatedDto dto)
        {
            var copy = new BookCopy
            {
                CopyNumber = dto.CopyNumber,
                IsAvailable = dto.IsAvailable
            };
            _context.BookCopies.Add(copy);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = copy.Id }, copy.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, BookCopyDto dto)
        {
            var copy = await _context.BookCopies.FindAsync(id);
            if (copy == null) return NotFound();

            copy.CopyNumber = dto.CopyNumber;
            copy.IsAvailable = dto.IsAvailable;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var copy = await _context.BookCopies.FindAsync(id);
            if (copy == null) return NotFound();

            _context.BookCopies.Remove(copy);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}