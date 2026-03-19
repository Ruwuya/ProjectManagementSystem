using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProjectManagement.Infrastructure.Repositories
{
    // ProjectRepository is responsible for data access related to projects,
    // including retrieving project details, adding notes, and managing sessions.
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection is missing.");
        }

        public async Task<ProjectDetailsDto?> GetProjectDetailsAsync(int projectId)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand("sp_GetProjectDetails", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ProjectId", SqlDbType.Int).Value = projectId;

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            ProjectDetailsDto? project = null;

            // Result set 1: Project
            // This result set contains the project details
            if (await reader.ReadAsync())
            {
                project = new ProjectDetailsDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("Description")),
                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                    ProjectStatus = reader.GetString(reader.GetOrdinal("ProjectStatus")),
                    CreatedByUserName = reader.GetString(reader.GetOrdinal("CreatedByUsername")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName"))
                };
            }

            if (project is null)
                return null;

            // Result set 2: Sessions
            // This result set contains the sessions associated with the project
            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                project.Sessions.Add(new ProjectSessionsDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    CheckIn = reader.GetDateTime(reader.GetOrdinal("CheckIn")),
                    CheckOut = reader.IsDBNull(reader.GetOrdinal("CheckOut"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("CheckOut")),
                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("Notes"))
                });
            }

            // Result set 3: Notes
            // This result set contains the notes associated with the project
            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                project.Notes.Add(new ProjectNoteDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    NoteText = reader.GetString(reader.GetOrdinal("NoteText")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                });
            }

            // Result set 4: Materials
            // This result set contains the materials associated with the project
            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                project.Materials.Add(new ProjectMaterialDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    MaterialName = reader.GetString(reader.GetOrdinal("MaterialName")),
                    QuantityUsed = reader.GetInt32(reader.GetOrdinal("QuantityUsed")),
                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Notes"))
                });
            }

            return project;
        }

        public async Task AddProjectNoteAsync(AddProjectNoteDto dto)
        {
            // Using ADO.NET to execute a stored procedure for adding a project note
            await using var connection = new SqlConnection(_connectionString);
            // Using a stored procedure to add a project note
            // The stored procedure 'sp_AddProjectNote' should be defined in the database with parameters:
            // @ProjectId INT, @UserId INT, @NoteText NVARCHAR(MAX)
            await using var command = new SqlCommand("sp_AddProjectNote", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ProjectId", SqlDbType.Int).Value = dto.ProjectId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = dto.UserId;
            // Using NVARCHAR(MAX) for note text to allow for long notes
            // But instead of max you write -1 which is the same as max in this context
            command.Parameters.Add("@NoteText", SqlDbType.NVarChar, -1).Value = dto.NoteText;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task CheckInSessionAsync(CheckInSessionDto dto)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand("sp_CheckInSession", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserId", SqlDbType.Int).Value = dto.UserId;
            command.Parameters.Add("@ProjectId", SqlDbType.Int).Value = dto.ProjectId;

            command.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = dto.Notes
                ?? (object)DBNull.Value;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task CheckOutSessionAsync(CheckOutSessionDto dto)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand("sp_CheckOutSession", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserId", SqlDbType.Int).Value = dto.UserId;

            command.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = dto.Notes
                ?? (object)DBNull.Value;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        public async Task<List<ProjectListItemDto>> GetAllProjectsAsync()
        {
            var projects = new List<ProjectListItemDto>();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(@"
                SELECT
                    Id,
                    Name,
                    CASE
                        WHEN EndDate < SYSDATETIME() THEN 'Completed'
                        WHEN StartDate > SYSDATETIME() THEN 'Upcoming'
                        ELSE 'Ongoing'
                    END AS Status
                FROM projects
                ORDER BY Name;", connection);

            command.CommandType = CommandType.Text;

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                projects.Add(new ProjectListItemDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Status = reader.GetString(reader.GetOrdinal("Status"))
                });
            }
            return projects;
        }
    }
}
