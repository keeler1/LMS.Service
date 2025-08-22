namespace Web.LMS.Service.dto;

public class LoanDto
{
    public Guid Id { get; set; }
    public string MemberName { get; set; } = default!;
    public string BookTitle { get; set; } = default!;
    public string CopyNumber { get; set; } = default!;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}
