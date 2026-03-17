using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProjectService _projectService;
        public MainWindow(IProjectService projectService)
        {
            InitializeComponent();
            _projectService = projectService;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            var project = await _projectService.GetProjectDetailsAsync(1);

            if (project != null)
            {
                MessageBox.Show($"Project: {project.Name}\nSessions: {project.Sessions.Count}");
            }
            else
            {
                MessageBox.Show("Project not found.");
            }
        }
    }
}