using System.ComponentModel.DataAnnotations;
using static MOH.Models.Enum;

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
        [Required]
        public Status status { get; set; } = Status.Active;
        [Required]
        public UserType UserType { get; set; } = UserType.GeneralUser;
    }
}
