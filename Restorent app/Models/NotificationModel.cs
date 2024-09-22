using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restorent_app.Models
{
    public class NotificationModel
    {
        [Key]
        public int NotificationId { get; set; }  // Primary Key

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }  // Foreign Key to UserModel
        public UserModel User { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }  // Foreign Key to RestaurantModel
        public RestaurantModel Restaurant { get; set; }

        [Required]
        [MaxLength(250)]
        public string Message { get; set; }

        public bool IsUserSide { get; set; }
    }
}
