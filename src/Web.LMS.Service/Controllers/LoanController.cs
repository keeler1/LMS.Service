using Infrastructure.LMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.LMS.Service.dto;

namespace Web.LMS.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LoanController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<LoanDto>>> Get()
        {
            var loans = await _context.Loans
                .Include(l => l.Member)
                .Include(l => l.BookCopy)
                    .ThenInclude(c => c.Book)
                .Take(5)
                .OrderBy(l => l.LoanDate)
                .ToListAsync();

            var dto = loans.Select(l => new LoanDto
            {
                Id = l.Id,
                MemberName = $"{l.Member.FirstName} {l.Member.LastName}",
                BookTitle = l.BookCopy.Book.Title,
                CopyNumber = l.BookCopy.CopyNumber,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> Get(Guid id)
        {
            var loan = await _context.Loans
                .Include(l => l.Member)
                .Include(l => l.BookCopy)
                    .ThenInclude(c => c.Book)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (loan == null) return NotFound();

            var dto = new LoanDto
            {
                Id = loan.Id,
                MemberName = $"{loan.Member.FirstName} {loan.Member.LastName}",
                BookTitle = loan.BookCopy.Book.Title,
                CopyNumber = loan.BookCopy.CopyNumber,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnedDate = loan.ReturnedDate
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(LoanCreatedDto dto)
        {
            var loan = new Loan
            {
                MemberId = dto.MemberId,
                BookCopyId = dto.BookCopyId,
                LoanDate = dto.LoanDate,
                DueDate = dto.DueDate,
                ReturnedDate = dto.ReturnedDate
            };
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = loan.Id }, loan.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, LoanCreatedDto dto)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null) return NotFound();

            loan.MemberId = dto.MemberId;
            loan.BookCopyId = dto.BookCopyId;
            loan.LoanDate = dto.LoanDate;
            loan.DueDate = dto.DueDate;
            loan.ReturnedDate = dto.ReturnedDate;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null) return NotFound();

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}