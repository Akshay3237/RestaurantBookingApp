using System.ComponentModel.DataAnnotations;

namespace Restorent_app.Models
{
    public class UserModel
    {
        [Key] // Assuming UserId is the primary key
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)] // Surname can be optional or with a max length
        public string Surname { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)] // Password should have a minimum length
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNo { get; set; }
    }
}
