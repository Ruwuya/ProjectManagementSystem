using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.DTOs
{
    public class AddProjectNoteDto
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public required string NoteText { get; set; }
    }
}
