using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Application.DTOs;

namespace ProjectManagement.Application.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectListItemDto>> GetAllProjectsAsync();
        Task<ProjectDetailsDto?> GetProjectDetailsAsync(int projectId);
        Task CheckInSessionAsync(int userId, int projectId, string? notes);
        Task CheckOutSessionAsync(int userId, string? notes);
        Task AddProjectNoteAsync(int projectId, int userId, string noteText);
    }
}
