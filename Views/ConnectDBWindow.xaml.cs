using DataFaker.Models;
using DataFaker.ViewModels;
using Wpf.Ui.Controls;

namespace DataFaker.Views
{
    /// <summary>
    /// Interaction logic for ConnectDBWindow.xaml
    /// </summary>
    public partial class ConnectDBWindow : FluentWindow
    {
        public ConnectDBWindow()
        {
            InitializeComponent();
            var connectDBViewModel = new ConnectDBViewModel();
            DataContext = connectDBViewModel;
            connectDBViewModel.ConnectionResult += ConnectDBSuccess;
            
        }

        private void ConnectDBSuccess(Object? sender, List<Schema> schemas)
        {
            var mainWindow = new MainWindow(schemas);
            mainWindow.Show();
            this.Close();
        }
    }
}
