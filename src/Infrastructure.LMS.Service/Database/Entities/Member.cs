namespace Infrastructure.LMS.Service;

public class Member
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    // Navigation
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
