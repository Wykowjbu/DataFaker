using DataFaker.Config;
using DataFaker.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFaker.Services
{
    public class ConnectDBService
    {

        private readonly DatabaseConfig _databaseConfig;
        public ConnectDBService(Database database) 
        {
            _databaseConfig = new DatabaseConfig(database.GetConnectionString());
        }

        public async Task<List<Schema>> GetSchemasAsync()
        {
            var schemas = new List<Schema>();
            using var conn = _databaseConfig.GetConnection();
            await conn.OpenAsync();
            string query = "SELECT schema_name FROM information_schema.schemata WHERE schema_name NOT IN ('information_schema', 'pg_catalog', 'pg_toast');";
            using (var cmd = new NpgsqlCommand(query, conn))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var schema = new Schema
                    {
                        Name = reader.GetString(0)
                    };
                    schemas.Add(schema);
                }

            }
                
            // Sau khi lấy xong tất cả schema, ta sẽ lấy bảng cho từng schema chứ không là lỗi ( vì chưa đóng reder khi đọc schema)
            foreach (var schema in schemas)
            {
                schema.Tables = await GetTablesAsync(conn,schema.Name);
            }
            return schemas;

        }

        private async Task<List<Table>> GetTablesAsync(NpgsqlConnection conn,string schemaName)
        {
            var tables = new List<Table>();
            string query = $"SELECT table_name FROM information_schema.tables WHERE table_schema = '{schemaName}';";
            using (var cmd = new NpgsqlCommand(query, conn))
            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var table = new Table
                    {
                        Name = reader.GetString(0)
                    };
                    tables.Add(table);
                }

            // Lấy cột cho từng bảng
            foreach (var table in tables)
            {
                table.Columns = await GetColumnsAsync(conn, schemaName, table.Name);
            }
            return tables;
            
        }



        private async Task<List<Column>> GetColumnsAsync(NpgsqlConnection conn, string schemaName, string tableName)
        {
            var columns = new List<Column>();
            string query = $"SELECT column_name, data_type FROM information_schema.columns WHERE table_schema = '{schemaName}' AND table_name = '{tableName}';";
            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var column = new Column
                {
                    Name = reader.GetString(0),
                    DataType = reader.GetString(1)
                };
                columns.Add(column);
            }
            return columns;
        }

    }
}
