namespace DataFaker.Models
{
    public class Column
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsNotNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsAutoIncrement { get; set; }
        public Column() { }
    }
}
