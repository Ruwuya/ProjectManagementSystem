using System.Windows;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Wpf.ViewModels;

namespace ProjectManagement.Wpf.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(IProjectService projectService)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(projectService);
        }
    }
}