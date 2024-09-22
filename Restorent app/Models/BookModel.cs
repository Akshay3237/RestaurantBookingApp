using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Restorent_app.Models
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }  // Primary Key

        [Required]
        [ForeignKey("Table")]
        public int TableId { get; set; }  // Foreign Key to TableModel
        public TableModel Table { get; set; }

        [Required]
        public DateTime StartTime { get; set; }  // Start time for the booking

        [Required]
        public DateTime EndTime { get; set; }
    }
}
