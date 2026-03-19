using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.Wpf.ViewModels
{
    public class ProjectSummaryViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;
        private readonly int _projectId;

        private string _projectName = "Loading...";
        public string ProjectName
        {
            get => _projectName;
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }

        private string _status = "Unknown";
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private string _createdBy = string.Empty;
        public string CreatedBy
        {
            get => _createdBy;
            set
            {
                _createdBy = value;
                OnPropertyChanged();
            }
        }

        private int _sessionsCount;
        public int SessionsCount
        {
            get => _sessionsCount;
            set
            {
                _sessionsCount = value;
                OnPropertyChanged();
            }
        }

        private int _notesCount;
        public int NotesCount
        {
            get => _notesCount;
            set
            {
                _notesCount = value;
                OnPropertyChanged();
            }
        }

        private int _materialsCount;
        public int MaterialsCount
        {
            get => _materialsCount;
            set
            {
                _materialsCount = value;
                OnPropertyChanged();
            }
        }

        public ProjectSummaryViewModel(IProjectService projectService, int projectId)
        {
            _projectService = projectService;
            _projectId = projectId;

            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            var project = await _projectService.GetProjectDetailsAsync(_projectId);

            if (project is null)
            {
                ProjectName = "Project not found";
                Status = "Unknown";
                CreatedBy = string.Empty;
                SessionsCount = 0;
                NotesCount = 0;
                MaterialsCount = 0;
                return;
            }

            ProjectName = project.Name;
            Status = project.ProjectStatus;
            CreatedBy = $"{project.FirstName} {project.LastName} ({project.CreatedByUserName})";
            SessionsCount = project.Sessions.Count;
            NotesCount = project.Notes.Count;
            MaterialsCount = project.Materials.Count;
        }
    }
}