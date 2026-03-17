using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.DTOs
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectStatus { get; set; } = string.Empty;
        public string CreatedByUserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<ProjectSessionsDto> Sessions { get; set; } = new();
        public List<ProjectNoteDto> Notes { get; set; } = new();
        public List<ProjectMaterialDto> Materials { get; set; } = new();

    }
}
