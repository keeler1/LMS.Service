namespace Web.LMS.Service.dto;

public class LoanCreatedDto
{
    public Guid MemberId { get; set; }
    public Guid BookCopyId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}
