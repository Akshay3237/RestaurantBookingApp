using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restorent_app.Models
{
    public class RestaurantModel
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        [StringLength(100)]
        public string RestaurantName { get; set; }

        [Required]
        [StringLength(50)]
        public string RestaurantUniqueName { get; set; } // Unique constraint will be set in DbContext

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        [Required]
        public RestaurantType Type { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        [ForeignKey("ManagerId")]
        public int ManagerId { get; set; }

        public UserModel Manager { get; set; }
    }

    public enum RestaurantType
    {
        Veg,
        NonVeg,
        Both
    }
}
