using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Domain.Entities.Models
{
    [Table("project_materials")]
    public class ProjectMaterial
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("ProjectId")]
        public int ProjectId { get; set; }

        [Required]
        [Column("MaterialId")]
        public int MaterialId { get; set; }

        [Required]
        [Column("QuantityUsed")]
        public int QuantityUsed { get; set; }
    }
}
