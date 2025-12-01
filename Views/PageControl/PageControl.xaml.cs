using DataFaker.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using Table = DataFaker.Models.Table;

namespace DataFaker.Views.PageControl
{
    /// <summary>
    /// Interaction logic for PageControl.xaml
    /// </summary>
    public partial class PageControl : UserControl
    {
        public PageControl()
        {
            InitializeComponent();
        }

        private void Table_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid?.SelectedItem is Table table)
            {
                if (DataContext is PageViewModel viewModel)
                {
                    // Execute the command
                    if (viewModel.OpenTabCommand.CanExecute(table))
                    {
                        viewModel.OpenTabCommand.Execute(table);
                    }
                }
            }
        }
    }
}
