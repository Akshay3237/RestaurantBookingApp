using System.ComponentModel.DataAnnotations;
using System;

namespace Restorent_app.Models
{
    public class WaitingModel
    {
        [Key]
        public int WaitingId { get; set; }  // Primary Key

        [Required]
        public int TableId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public DateTime? PriorityTime { get; set; }
    }
}
