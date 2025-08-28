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

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Required]
        public int Age { get; set; }
        public string? Gender { get; set; }
    }
}
