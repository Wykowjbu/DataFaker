using DataFaker.Models;

namespace DataFaker.Services
{
    public class DatabaseService
    {
        public async Task<(bool Success, List<Schema> Schemas)> ConnectAsync(Database database)
        {
            try
            {
                var connectDBService = new ConnectDBService(database);
                return (true, await connectDBService.GetSchemasAsync());
            }
            catch (Exception)
            {
                return (false, new List<Schema>());
            }
        }
    }
}
