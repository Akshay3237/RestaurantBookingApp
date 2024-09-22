using System.ComponentModel.DataAnnotations;

namespace Restorent_app.Models
{
    public class NotificationModel
    {
        [Key]
        public int NotificationId { get; set; }  

        [Required]
        public int UserId { get; set; }  

        [Required]
        public int RestaurantId { get; set; }  

        [Required]
        [MaxLength(250)]
        public string Message { get; set; }

        public bool IsUserSide { get; set; }
    }
}
