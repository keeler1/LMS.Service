namespace Web.LMS.Service.dto;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!; // Flattened Author name
    public string ISBN { get; set; } = default!;
    public int PublishedYear { get; set; }
    public string? Publisher { get; set; }
    public ICollection<BookCopyDto> Copies { get; set; } = new List<BookCopyDto>();

}
