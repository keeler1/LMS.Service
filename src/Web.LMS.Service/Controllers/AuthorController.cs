using Infrastructure.LMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.LMS.Service.dto;

namespace Web.LMS.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> Get()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .Take(5)
                .OrderBy(a => a.Name)
                .ToListAsync();

            var dto = authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Books = a.Books.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Publisher = b.Publisher,
                }).ToList()
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get(Guid id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();

            var dto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Books = author.Books.Select(b => new BookDto { Id = b.Id, Title = b.Title, Publisher = b.Publisher }).ToList()
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(AuthorCreatedDto dto)
        {
            var author = new Author
            {
                Name = dto.Name
            };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = author.Id }, author.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AuthorCreatedDto dto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            author.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}