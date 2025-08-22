namespace Infrastructure.LMS.Service;

public class BookCopy
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BookId { get; set; }
    public Book Book { get; set; } = default!;

    public string CopyNumber { get; set; } = default!; // e.g., "C-001"
    public bool IsAvailable { get; set; } = true;

    // Navigation
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
