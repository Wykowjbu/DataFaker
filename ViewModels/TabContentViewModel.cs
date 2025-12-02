using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataFaker.Models;
using DataFaker.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace DataFaker.ViewModels
{
    public partial class TabContentViewModel : ObservableObject
    {
        private GenDataService genDataService;

        [ObservableProperty]
        private ObservableCollection<ColumnPlus> columnsPluss = new();

        [ObservableProperty]
        private bool isDisplayFilter = true;

        [ObservableProperty]
        private string applyOrReset = "Reset";

        [ObservableProperty]
        private string filterText;

        partial void OnFilterTextChanged(string value)
        {
            if (value == null || value.Trim() == "")
            {
                ApplyOrReset = "Reset";
            }
            else
            {
                ApplyOrReset = "Apply";
            }
        }

        public TabContentViewModel(List<Column> columns)
        {
            genDataService = new GenDataService();
            foreach (var column in columns)
            {
                ColumnsPluss.Add(new ColumnPlus(column));
            }
            var gen = new GenDataService();
            foreach (var column in ColumnsPluss)
            {
                column.Value = gen.GetData(column);
            }
        }

        [RelayCommand]
        private void Filter()
        {
            IsDisplayFilter = !IsDisplayFilter;
        }

        [RelayCommand]
        private void GenData()
        {
            foreach (var column in ColumnsPluss)
            {
                column.Value = genDataService.GetData(column);            
            }
            Clipboard.SetText(string.Join("\t", ColumnsPluss.Select(c => c.Value?.ToString() ?? "")));
        }

    }
}
