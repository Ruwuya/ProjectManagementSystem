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
        [Key]
        [Column("UserId")]
        public string UserId { get; set; }
        [Key]
        [Column("ProjectId")]
        public int ProjectId { get; set; }
        [Key]
        [Column("CheckIn")]
        public DateTime CheckIn { get; set; }
        [Key]
        [Column("CheckOut")]
        public DateTime CheckOut { get; set; }
    }
}
