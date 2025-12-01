using CommunityToolkit.Mvvm.ComponentModel;
using DataFaker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFaker.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Schema> schemas;

        public MainViewModel(List<Schema> schemas)
        {
            Schemas =new ObservableCollection<Schema>(schemas);
        }

    }
}
