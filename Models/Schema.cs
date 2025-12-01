namespace DataFaker.Models
{
    public class Schema
    {
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        public Schema()
        {
            Tables = new List<Table>();
        }
    }
}
