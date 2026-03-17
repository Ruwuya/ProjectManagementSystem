using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.DTOs
{
    public class ProjectSessionsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime? Checkout { get; set; }
        public string? Notes { get; set; }
    }
}
