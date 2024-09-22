using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restorent_app.Models
{
    public class WaitingModel
    {
        [Key]
        public int WaitingId { get; set; }  // Primary Key

        [Required]
        [ForeignKey("Table")]
        public int TableId { get; set; }  // Foreign Key to TableModel
        public TableModel Table { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public DateTime? PriorityTime { get; set; }
    }
}
