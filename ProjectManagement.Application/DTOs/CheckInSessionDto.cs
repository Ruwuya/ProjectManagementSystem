using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.DTOs
{
    public class CheckInSessionDto
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string? Notes { get; set; }
    }
}
