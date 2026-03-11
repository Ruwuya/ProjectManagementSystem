using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Domain.Entities.Models
{
    [Table("project_members")]
    public class ProjectMember
    {
        [Required]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("ProjectId")]
        public int ProjectId { get; set; }

        [Required]
        [Column("UserId")]
        public int UserId { get; set; }

        [Required]
        [Column("ProjectRoleId")]
        public int ProjectRoleId { get; set; }

        [Required]
        [Column("JoinedAt")]
        public DateTime JoinedAt { get; set; }

        [Column("LeftAt")]
        public DateTime LeftAt { get; set; }

        [Required]
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
