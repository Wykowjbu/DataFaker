using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataFaker.Models;
using System.Collections.ObjectModel;

namespace DataFaker.ViewModels
{
    public partial class PageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Schema? selectedSchema;

        [ObservableProperty]
        private ObservableCollection<Schema> schemas = new();

        [ObservableProperty]
        private ObservableCollection<TabViewModel> tabs = new();

        [ObservableProperty]
        private TabViewModel selectedTab;


        public PageViewModel(List<Schema> schemas)
        {
            foreach (var schema in schemas)
            {
                Schemas.Add(schema);
            }
        }

        [RelayCommand]
        private async Task CloseTab(TabViewModel tab)
        {
            if (tab != null && Tabs.Contains(tab))
            {
                Tabs.Remove(tab);
                if (SelectedTab == tab && Tabs.Count > 0)
                {
                    SelectedTab = Tabs[^1];
                }
            }
        }

        [RelayCommand]
        private async Task OpenTab(Table table)
        {
            var tabViewModel = new TabViewModel(table);
            Tabs.Add(tabViewModel);
            SelectedTab = tabViewModel;

        }

    }
}
