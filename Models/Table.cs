namespace DataFaker.Models
{
    public  class Table
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
        public Table()
        {
            Columns = new List<Column>();
        }
    }
}
