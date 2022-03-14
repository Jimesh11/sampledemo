using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDemoApp.Models
{
    public class Users
    {
        [Key]
        public long UserId { get; set; } 
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Gender { get; set; } 
        public string Designation { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; } 
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; } 
        public bool? isActive { get; set; } 
    }
}
