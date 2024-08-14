using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookManagementAPI.Domain.Entities
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public int? PublisherId { get; set; }
        public int? CategoryID { get; set; }

        //navigation purpose
        public Author? Author { get; set; }
        public Publisher? Publisher { get; set; }
        public Category? Category { get; set; }
    }
}
