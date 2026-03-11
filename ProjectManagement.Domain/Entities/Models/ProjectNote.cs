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
        [Key]
        [Column("ProjectId")]
        public int ProjectId { get; set; }
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }
        [Key]
        [Column("NoteText")]
        public string NoteText { get; set; }
        [Key]
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
