namespace Infrastructure.LMS.Service;

public class Staff
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = "Librarian";
}
