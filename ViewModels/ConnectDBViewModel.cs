using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataFaker.Models;
using DataFaker.Services;
using System.Windows.Media;

namespace DataFaker.ViewModels
{
    public partial class ConnectDBViewModel : ObservableObject
    {
        [ObservableProperty] private string databaseName;

        [ObservableProperty] private string username;

        [ObservableProperty] private string password;

        [ObservableProperty] private string host;

        [ObservableProperty] private string port;

        [ObservableProperty] private string message;

        [ObservableProperty] private Brush messageColor;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        private bool isLoading;

        public bool IsNotLoading => !isLoading;

        private readonly DatabaseService _databaseService;

        public event EventHandler<List<Schema>>? ConnectionResult;  

        public ConnectDBViewModel()
        {
            Host = "localhost";
            Port = "5432";
            _databaseService = new DatabaseService();
            Connect = new AsyncRelayCommand(RunConnectAsync);
        }

        public IAsyncRelayCommand Connect { get; }

        private async Task RunConnectAsync()
        {
            IsLoading = true;
            Message = "Connecting...";
            var database = new Database
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Name = databaseName

            };
            var result = await _databaseService.ConnectAsync(database);
            if (result.Success)
            {
                Message="Connection successful! Loading data...";
                MessageColor = Brushes.Green;
                await Task.Delay(3000);
                ConnectionResult?.Invoke(this, result.Schemas);
            }
            else
            {
                Message="Connection failed. Please check your credentials and try again.";
                MessageColor = Brushes.Red;
            }
            IsLoading = false;

        }
    }
}
