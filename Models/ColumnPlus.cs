using CommunityToolkit.Mvvm.ComponentModel;
using DataFaker.Models;

public partial class ColumnPlus : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string dataType;

    [ObservableProperty]
    private bool isPrimaryKey;

    [ObservableProperty]
    private bool isForeignKey;

    [ObservableProperty]
    private bool isAutoIncrement;

    [ObservableProperty]
    private bool isLocked;

    [ObservableProperty]
    private object value;

    public ColumnPlus(Column c)
    {
        name = c.Name;
        dataType = c.DataType;
        isPrimaryKey = c.IsPrimaryKey;
        isForeignKey = c.IsForeignKey;
        isAutoIncrement = c.IsAutoIncrement;
        isLocked = false;
    }
}
