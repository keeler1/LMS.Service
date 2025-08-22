namespace Infrastructure.LMS.Service;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = default!;
    public Author Author { get; set; } = default!;
    public Guid AuthorId { get; set; }
    public string ISBN { get; set; } = default!;
    public int PublishedYear { get; set; }
    public string? Publisher { get; set; }

    // Navigation
    public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
}
