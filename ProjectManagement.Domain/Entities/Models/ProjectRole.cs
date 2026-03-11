using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Domain.Entities.Models
{
    [Table("project_roles")]
    public class ProjectRole
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Key]
        [Column("Name")]
        public string Name { get; set; }
    }
}
