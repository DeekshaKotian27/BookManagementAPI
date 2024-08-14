using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<Books>? Books { get; set; }
    }
}