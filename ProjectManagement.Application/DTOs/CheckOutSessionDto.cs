using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.DTOs
{
    public class CheckOutSessionDto
    {
        public int UserId { get; set;  }
        public string? Notes { get; set; }
    }
}
