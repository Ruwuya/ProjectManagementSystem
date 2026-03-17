using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.DTOs
{
    public class ProjectMaterialDto
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public int QuantityUsed { get; set; }
        public string? Notes { get; set; }
    }
}
