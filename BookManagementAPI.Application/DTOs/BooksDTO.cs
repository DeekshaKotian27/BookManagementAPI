namespace BookManagementAPI.Application.DTOs
{
    public class BooksDTO
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }
    }

    public class ResponseBooksDTO
    {
        public int BookID { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? PublisherName { get; set; } = string.Empty;
        public int PublisherId { get; set; }
        public string? AuthorName { get; set; } = string.Empty;
        public int AuthorId { get; set; }

    }

    public class ResponsePublisherBookDTO
    {
        public int BookID { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? AuthorName { get;set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
    }

    public class ResponseCategoryBookDTO
    {
        public int BookID { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? AuthorName { get; set; } = string.Empty;
        public string? PublisherName { get; set; } = string.Empty;
    }

    public class ResponseAuthorBookDTO
    {
        public int BookID { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? PublisherName { get; set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
    }
}
