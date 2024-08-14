using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Domain.Entities
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherAddress { get; set; }
        public string ? PublisherPhoneNumber { get; set; }
        public string? PublisherEmailId { get; set; }
        public List<Books>? Books { get; set; }
    }
}
