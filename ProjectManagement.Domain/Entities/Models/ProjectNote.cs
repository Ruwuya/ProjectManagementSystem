using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Domain.Entities.Models
{
    [Table("project_notes")]
    public class ProjectNote
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("ProjectId")]
        public int ProjectId { get; set; }

        [Required]
        [Column("UserId")]
        public int UserId { get; set; }

        [Required]
        [Column("NoteText")]
        public string NoteText { get; set; }

        [Required]
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
