namespace DataFaker.Models
{
    public class Database
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Schema> Schemas { get; set; }
        public Database()
        {
            Schemas = new List<Schema>();
        }
        public string GetConnectionString()
        {
            return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Name};";
        }
    }
}
