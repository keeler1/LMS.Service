namespace Web.LMS.Service.dto;

public class MemberDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public ICollection<LoanDto> Loans { get; set; } = new List<LoanDto>();
}
