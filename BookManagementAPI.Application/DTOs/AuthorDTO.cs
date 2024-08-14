namespace BookManagementAPI.Application.DTOs
{
    public class AuthorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ResponseAuthorDTO
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public List<ResponseAuthorBookDTO> Books { get; set; }
    }
}
