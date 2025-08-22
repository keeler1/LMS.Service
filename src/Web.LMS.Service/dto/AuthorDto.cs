using Infrastructure.LMS.Service;

namespace Web.LMS.Service.dto;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
}
