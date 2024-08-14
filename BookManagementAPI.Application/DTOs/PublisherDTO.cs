namespace BookManagementAPI.Application.DTOs
{
    public class PublisherDTO
    {
        public string? PublisherName { get; set; }
        public string? PublisherAddress { get; set; }
        public string? PublisherPhoneNumber { get; set; }
        public string? PublisherEmailId { get; set; }
    }

    public class ResponseMultiplePublisherDTO
    {
        public int PublisherID { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherAddress { get; set; }
        public string? PublisherPhoneNumber { get; set; }
        public string? PublisherEmailId { get; set; }
        public List<ResponsePublisherBookDTO> Books { get; set; }
    }
}
