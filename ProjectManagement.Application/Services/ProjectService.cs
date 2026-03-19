using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.Application.Services;

public class ProjectService : IProjectService
{
    // The ProjectService class is responsible for handling business logic related to projects,
    // such as adding notes and managing session check-ins and check-outs.
    // It relies on the IProjectRepository to interact with the data layer.
    private readonly IProjectRepository _projectRepository;

    // Constructor that takes an IProjectRepository as a dependency, allowing for loose coupling and easier testing.
    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    // Method to add a note to a project.
    // It creates an AddProjectNoteDto with the provided parameters and calls the repository method to save it.
    public async Task AddProjectNoteAsync(int projectId, int userId, string noteText)
    {
        // Create a DTO to encapsulate the data needed to add a project note.
        var dto = new AddProjectNoteDto
        {
            ProjectId = projectId,
            UserId = userId,
            NoteText = noteText
        };

        await _projectRepository.AddProjectNoteAsync(dto);
    }

    // Method to check in a session for a user on a specific project.
    public async Task CheckInSessionAsync(int userId, int projectId, string? notes)
    {
        // Create a DTO to encapsulate the data needed to check in a session.
        var dto = new CheckInSessionDto
        {
            UserId = userId,
            ProjectId = projectId,
            Notes = notes
        };

        await _projectRepository.CheckInSessionAsync(dto);
    }

    // Method to check out a session for a user.
    public async Task CheckOutSessionAsync(int userId, string? notes)
    {
        // Create a DTO to encapsulate the data needed to check out a session.
        var dto = new CheckOutSessionDto
        {
            UserId = userId,
            Notes = notes
        };

        await _projectRepository.CheckOutSessionAsync(dto);
    }

    // Method to retrieve detailed information about a project, including its sessions, notes, and materials.
    public async Task<ProjectDetailsDto?> GetProjectDetailsAsync(int projectId)
    {
        // Call the repository method to get the project details and return the result.
        return await _projectRepository.GetProjectDetailsAsync(projectId);
    }
    public async Task<List<ProjectListItemDto>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllProjectsAsync();
    }

}