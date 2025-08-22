namespace Web.LMS.Service.dto
{
    public class BookCreatedDto
    {
        public string Title { get; set; } = default!;
        public Guid AuthorId { get; set; }  // Link to existing author
        public string ISBN { get; set; } = default!;
        public int PublishedYear { get; set; }
        public string? Publisher { get; set; }
    }
}
