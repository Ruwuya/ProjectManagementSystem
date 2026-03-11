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
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Key]
        [Column("ProjectId")]
        public int ProjectId { get; set; }
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }
        [Key]
        [Column("ProjectRoleId")]
        public int ProjectRoleId { get; set; }
        [Key]
        [Column("JoinedAt")]
        public DateTime JoinedAt { get; set; }
        [Key]
        [Column("LeftAt")]
        public DateTime LeftAt { get; set; }
        [Key]
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
