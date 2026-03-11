using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Domain.Entities.Models
{
    [Table("sessions")]
    public class Session
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("UserId")]
        public string UserId { get; set; }

        [Required]
        [Column("ProjectId")]
        public int ProjectId { get; set; }

        [Required]
        [Column("CheckIn")]
        public DateTime CheckIn { get; set; }

        [Column("CheckOut")]
        public DateTime CheckOut { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }
    }
}
