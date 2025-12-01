using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataFaker.Models;
using System.Collections.ObjectModel;

namespace DataFaker.ViewModels
{
    public partial class TabContentViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Column> columns = new();

        [ObservableProperty]
        private bool isDisplayFilter = true;

        [ObservableProperty]
        private string filterText;

        public TabContentViewModel(List<Column> columns)
        {
            foreach (var column in columns)
            {
                Columns.Add(column);
            }
        }

        [RelayCommand]
        private async Task Filter()
        {
            IsDisplayFilter = !IsDisplayFilter;
        }
    }
}
