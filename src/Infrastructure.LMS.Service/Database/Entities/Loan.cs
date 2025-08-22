namespace Infrastructure.LMS.Service;

public class Loan
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MemberId { get; set; }
    public Member Member { get; set; } = default!;

    public Guid BookCopyId { get; set; }
    public BookCopy BookCopy { get; set; } = default!;

    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}
