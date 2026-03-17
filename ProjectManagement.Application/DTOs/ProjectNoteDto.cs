using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.DTOs
{
    public class ProjectNoteDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string NoteText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
