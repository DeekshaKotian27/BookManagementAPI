using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Domain.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        //navigation purpose
        public List<Books>? Books { get; set; }
    }
}
