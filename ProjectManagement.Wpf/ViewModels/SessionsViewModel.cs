using System.Collections.ObjectModel;
using System.Windows.Input;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.Wpf.ViewModels
{
    public class SessionsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;
        private readonly int _projectId;
        private readonly int _currentUserId = 1; // temporary until login/current-user exists

        private readonly List<SessionItemViewModel> _allSessions = new();

        public ObservableCollection<SessionItemViewModel> Sessions { get; } = new();

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

        private DateTime _selectedDate = DateTime.Today;
        public string DisplayDate => _selectedDate.ToShortDateString();

        public bool CanSessionActions => _selectedDate.Date == DateTime.Today;

        public ICommand PreviousDayCommand { get; }
        public ICommand TodayCommand { get; }
        public ICommand NextDayCommand { get; }
        public ICommand CheckInCommand { get; }
        public ICommand CheckOutCommand { get; }

        public SessionsViewModel(IProjectService projectService, int projectId)
        {
            _projectService = projectService;
            _projectId = projectId;

            PreviousDayCommand = new RelayCommand(_ => MoveDay(-1));
            TodayCommand = new RelayCommand(_ => GoToToday());
            NextDayCommand = new RelayCommand(_ => MoveDay(1));
            CheckInCommand = new RelayCommand(async _ => await CheckInAsync(), _ => CanSessionActions);
            CheckOutCommand = new RelayCommand(async _ => await CheckOutAsync(), _ => CanSessionActions);

            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            var project = await _projectService.GetProjectDetailsAsync(_projectId);

            _allSessions.Clear();
            Sessions.Clear();

            if (project is null)
            {
                ProjectName = "Project not found";
                return;
            }

            ProjectName = project.Name;

            foreach (var session in project.Sessions)
            {
                _allSessions.Add(new SessionItemViewModel
                {
                    Username = session.Username,
                    CheckIn = session.CheckIn,
                    CheckOut = session.Checkout,
                    Notes = session.Notes ?? string.Empty
                });
            }

            ApplyDateFilter();
        }

        private void ApplyDateFilter()
        {
            Sessions.Clear();

            foreach (var session in _allSessions
                         .Where(s => s.CheckIn.Date == _selectedDate.Date)
                         .OrderBy(s => s.CheckIn))
            {
                Sessions.Add(session);
            }

            OnPropertyChanged(nameof(DisplayDate));
            OnPropertyChanged(nameof(CanSessionActions));

            if (CheckInCommand is RelayCommand checkInRelay)
                checkInRelay.RaiseCanExecuteChanged();

            if (CheckOutCommand is RelayCommand checkOutRelay)
                checkOutRelay.RaiseCanExecuteChanged();
        }

        private void MoveDay(int days)
        {
            _selectedDate = _selectedDate.AddDays(days);
            ApplyDateFilter();
        }

        private void GoToToday()
        {
            _selectedDate = DateTime.Today;
            ApplyDateFilter();
        }

        private async Task CheckInAsync()
        {
            await _projectService.CheckInSessionAsync(_currentUserId, _projectId, string.Empty);
            await LoadAsync();
        }

        private async Task CheckOutAsync()
        {
            await _projectService.CheckOutSessionAsync(_currentUserId, string.Empty);
            await LoadAsync();
        }
    }

    public class SessionItemViewModel
    {
        public string Username { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Notes { get; set; } = string.Empty;

        public string CheckInText => CheckIn.ToString("HH:mm");
        public string CheckOutText => CheckOut?.ToString("HH:mm") ?? "-";
    }
}