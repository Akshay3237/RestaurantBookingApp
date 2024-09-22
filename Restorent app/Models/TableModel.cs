using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restorent_app.Models
{
    public class TableModel
    {
        [Key]
        public int Tableid { get; set; } // Primary key

        [Required]
        public int RestaurantId { get; set; } // Foreign key reference to Restaurant

        [Required]
        [Range(1, 20)] // Assuming a reasonable range for table capacity
        public int Capacity { get; set; }

        [Required]
        public int NumberOfTable { get; set; } // Indicates how many tables of this type exist

        // Navigation property for the associated Restaurant
        [ForeignKey("RestaurantId")]
        public RestaurantModel Restaurant { get; set; }
    }
}
