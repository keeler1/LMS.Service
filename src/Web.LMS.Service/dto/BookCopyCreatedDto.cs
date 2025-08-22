namespace Web.LMS.Service.dto;

public class BookCopyCreatedDto
{
    public string CopyNumber { get; set; } = default!;
    public Guid BookId { get; set; }
    public bool IsAvailable { get; set; }
}
