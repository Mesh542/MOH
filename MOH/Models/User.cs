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
    }
}
