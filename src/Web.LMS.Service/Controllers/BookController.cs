using Infrastructure.LMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.LMS.Service.dto;

namespace Web.LMS.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> Get()
        {
            var books = await _context.Books
                .Include(b => b.Copies)
                .Include(b => b.Author)
                .Take(5)
                .OrderBy(b => b.Title)
                .ToListAsync();

            var dto = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author.Name,
                ISBN = b.ISBN,
                PublishedYear = b.PublishedYear,
                Publisher = b.Publisher,
                Copies = b.Copies.Select(c => new BookCopyDto
                {
                    Id = c.Id,
                    CopyNumber = c.CopyNumber,
                    IsAvailable = c.IsAvailable
                }).ToList()
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> Get(Guid id)
        {
            var b = await _context.Books.Include(b => b.Copies)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (b == null) return NotFound();

            var dto = new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author.Name,
                ISBN = b.ISBN,
                PublishedYear = b.PublishedYear,
                Publisher = b.Publisher,
                Copies = b.Copies.Select(c => new BookCopyDto
                {
                    Id = c.Id,
                    CopyNumber = c.CopyNumber,
                    IsAvailable = c.IsAvailable
                }).ToList()
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(BookCreatedDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                AuthorId = dto.AuthorId,
                ISBN = dto.ISBN,
                PublishedYear = dto.PublishedYear,
                Publisher = dto.Publisher
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BookCreatedDto dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.Title = dto.Title;
            book.AuthorId = dto.AuthorId;
            book.ISBN = dto.ISBN;
            book.PublishedYear = dto.PublishedYear;
            book.Publisher = dto.Publisher;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}