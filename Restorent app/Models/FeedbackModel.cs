using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restorent_app.Models
{
    public class FeedbackModel
    {
        [Key]
        public int FeedbackId { get; set; }  // Primary Key

        [ForeignKey("User")]
        public int UserId { get; set; }  // Foreign Key to UserModel
        public UserModel User { get; set; }


        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }  // Foreign Key to RestaurantModel
        public RestaurantModel Restaurant { get; set; }

        [Range(1, 5)]
        public int RateNo { get; set; }  // Rating between 1 and 5

        [MaxLength(500)]
        public string Message { get; set; }
    }
}
