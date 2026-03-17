using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectDetailsDto?> GetProjectDetailsAsync(int projectId);
        Task AddProjectNoteAsync(AddProjectNoteDto dto);
        Task CheckInSessionAsync(CheckInSessionDto dto);
        Task CheckOutSessionAsync(CheckOutSessionDto dto);
    }
}
