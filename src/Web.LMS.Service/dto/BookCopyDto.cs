namespace Web.LMS.Service.dto;

public class BookCopyDto
{
    public Guid Id { get; set; }
    public string CopyNumber { get; set; } = default!;
    public string BookName { get; set; } = default!;
    public bool IsAvailable { get; set; }
    public ICollection<LoanDto> Loans { get; set; } = new List<LoanDto>();
}
