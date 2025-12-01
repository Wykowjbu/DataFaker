using CommunityToolkit.Mvvm.ComponentModel;
using DataFaker.Models;


namespace DataFaker.ViewModels
{
    public partial class TabViewModel : ObservableObject
    {
        [ObservableProperty]
        private string header;

        [ObservableProperty]
        private TabContentViewModel tabContentViewModel;

        public TabViewModel(Table table)
        {
            Header = table.Name;
            TabContentViewModel = new TabContentViewModel(table.Columns);
        }

    }
}
