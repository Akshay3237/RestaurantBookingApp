using System.ComponentModel.DataAnnotations;

namespace Restorent_app.Models
{
    public class FeedbackModel
    {
        [Key]
        public int FeedbackId { get; set; }  

        [Required]
        public int RestaurantId { get; set; }  

        [Required]
        public int UserId { get; set; }

        [Range(1, 5)]
        public int RateNo { get; set; } )

        [MaxLength(500)]
        public string Message { get; set; }
    }
}
