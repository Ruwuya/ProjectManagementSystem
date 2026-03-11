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
        [Key]
        [Column("ProjectId")]
        public int ProjectId { get; set; }
        [Key]
        [Column("MaterialId")]
        public int MaterialId { get; set; }
        [Key]
        [Column("QuantityUsed")]
        public int QuantityUsed { get; set; }
    }
}
