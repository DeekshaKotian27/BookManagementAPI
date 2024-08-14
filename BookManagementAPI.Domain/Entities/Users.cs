using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Domain.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? EmailID { get; set; }
        public string? Password { get; set;}
        public string? Role { get; set; }
    }
}
