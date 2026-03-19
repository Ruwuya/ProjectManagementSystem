using System.Collections.ObjectModel;
using System.Windows.Input;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.Wpf.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;
        private readonly int _projectId;
        private readonly int _currentUserId = 1; // temporary until login/current-user exists

        public ObservableCollection<NoteItemViewModel> Notes { get; } = new();

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

        private string _dateText = DateTime.Today.ToShortDateString();
        public string DateText
        {
            get => _dateText;
            set
            {
                _dateText = value;
                OnPropertyChanged();
            }
        }

        private string _newNoteText = string.Empty;
        public string NewNoteText
        {
            get => _newNoteText;
            set
            {
                _newNoteText = value;
                OnPropertyChanged();
                if (SendNoteCommand is RelayCommand relay)
                    relay.RaiseCanExecuteChanged();
            }
        }

        public ICommand SendNoteCommand { get; }

        public NotesViewModel(IProjectService projectService, int projectId)
        {
            _projectService = projectService;
            _projectId = projectId;

            SendNoteCommand = new RelayCommand(async _ => await SendNoteAsync(), _ => !string.IsNullOrWhiteSpace(NewNoteText));

            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            var project = await _projectService.GetProjectDetailsAsync(_projectId);

            Notes.Clear();

            if (project is null)
            {
                ProjectName = "Project not found";
                return;
            }

            ProjectName = project.Name;
            DateText = DateTime.Today.ToShortDateString();

            foreach (var note in project.Notes.OrderBy(n => n.CreatedAt))
            {
                Notes.Add(new NoteItemViewModel
                {
                    Username = note.Username,
                    Time = note.CreatedAt.ToString("HH:mm"),
                    Content = note.NoteText
                });
            }
        }

        private async Task SendNoteAsync()
        {
            if (string.IsNullOrWhiteSpace(NewNoteText))
                return;

            await _projectService.AddProjectNoteAsync(_projectId, _currentUserId, NewNoteText.Trim());

            NewNoteText = string.Empty;
            await LoadAsync();
        }
    }

    public class NoteItemViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}