using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MOH.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Surname { get; set; }
        [Required]
        public int Age { get; set; }
        public string? Gender { get; set; }


         // i have added this New fields for signup
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }


        // ✅ New field for activation (false by default)
        public bool IsActive { get; set; } = false;
    }
}
