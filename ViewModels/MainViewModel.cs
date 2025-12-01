using CommunityToolkit.Mvvm.ComponentModel;
using DataFaker.Models;



namespace DataFaker.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private PageViewModel pageViewModel;

        public MainViewModel(List<Schema> schemas)
        {
            PageViewModel = new PageViewModel(schemas);
        }
    }
}
