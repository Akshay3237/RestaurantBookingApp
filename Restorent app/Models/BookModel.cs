using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Restorent_app.Models
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }  // Primary Key

        [Required(ErrorMessage = "Table ID is required.")]
        [ForeignKey("Table")]
        public int TableId { get; set; }  // Foreign Key to TableModel
        public TableModel Table { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; }  // Start time for the booking

        [Required(ErrorMessage = "End time is required.")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }  // Foreign Key to User
        public UserModel User { get; set; } // Assuming you have a UserModel for user details
    }
}
