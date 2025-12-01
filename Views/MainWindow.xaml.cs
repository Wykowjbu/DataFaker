using DataFaker.Models;
using DataFaker.ViewModels;
using Wpf.Ui.Controls;

namespace DataFaker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        public MainWindow(List<Schema> schemas)
        {
            InitializeComponent();
            DataContext = new MainViewModel(schemas);
        }
    }
}
