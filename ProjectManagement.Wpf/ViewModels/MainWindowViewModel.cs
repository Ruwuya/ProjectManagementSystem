using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.Wpf.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        public ObservableCollection<ProjectListItemDto> Projects { get; } = new();

        private int _currentProjectId = 1;

        private object _currentViewModel = null!;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private bool _isHomeSelected;
        public bool IsHomeSelected
        {
            get => _isHomeSelected;
            set
            {
                _isHomeSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isNotesSelected;
        public bool IsNotesSelected
        {
            get => _isNotesSelected;
            set
            {
                _isNotesSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isSessionsSelected;
        public bool IsSessionsSelected
        {
            get => _isSessionsSelected;
            set
            {
                _isSessionsSelected = value;
                OnPropertyChanged();
            }
        }

        private ProjectListItemDto? _selectedProject;
        public ProjectListItemDto? SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged();

                if (value != null)
                {
                    OnProjectChanged(value.Id);
                }
            }
        }

        public ICommand ShowHomeCommand { get; }
        public ICommand ShowNotesCommand { get; }
        public ICommand ShowSessionsCommand { get; }

        public MainWindowViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            ShowHomeCommand = new RelayCommand(_ => ShowHome());
            ShowNotesCommand = new RelayCommand(_ => ShowNotes());
            ShowSessionsCommand = new RelayCommand(_ => ShowSessions());

            _ = LoadProjectsAsync();
        }

        private async Task LoadProjectsAsync()
        {
            Projects.Clear();

            var projects = await _projectService.GetAllProjectsAsync();

            foreach (var project in projects)
            {
                Projects.Add(project);
            }

            SelectedProject = Projects.FirstOrDefault();

            if (SelectedProject is null)
            {
                ShowHome();
            }
        }

        private void OnProjectChanged(int projectId)
        {
            _currentProjectId = projectId;

            if (IsHomeSelected)
                ShowHome();
            else if (IsNotesSelected)
                ShowNotes();
            else if (IsSessionsSelected)
                ShowSessions();
            else
                ShowHome();
        }

        private void ShowHome()
        {
            CurrentViewModel = new ProjectSummaryViewModel(_projectService, _currentProjectId);
            IsHomeSelected = true;
            IsNotesSelected = false;
            IsSessionsSelected = false;
        }

        private void ShowNotes()
        {
            CurrentViewModel = new NotesViewModel(_projectService, _currentProjectId);
            IsHomeSelected = false;
            IsNotesSelected = true;
            IsSessionsSelected = false;
        }

        private void ShowSessions()
        {
            CurrentViewModel = new SessionsViewModel(_projectService, _currentProjectId);
            IsHomeSelected = false;
            IsNotesSelected = false;
            IsSessionsSelected = true;
        }
    }
}