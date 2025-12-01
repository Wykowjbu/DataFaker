using DataFaker.Config;
using DataFaker.Models;
using Npgsql;

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

            string query = $@"
        SELECT 
            c.column_name,
            c.data_type,
            c.is_nullable,
            CASE WHEN tc.constraint_type = 'PRIMARY KEY' THEN true ELSE false END AS is_primary_key,
            fk.referenced_table,
            fk.referenced_column
        FROM information_schema.columns c
        LEFT JOIN information_schema.key_column_usage kcu
            ON c.table_schema = kcu.table_schema
            AND c.table_name = kcu.table_name
            AND c.column_name = kcu.column_name
        LEFT JOIN information_schema.table_constraints tc
            ON kcu.constraint_name = tc.constraint_name
            AND kcu.table_schema = tc.table_schema
            AND kcu.table_name = tc.table_name
            AND tc.constraint_type = 'PRIMARY KEY'
        LEFT JOIN (
            SELECT
                kcu.column_name,
                kcu.table_name,
                kcu.table_schema,
                ccu.table_name AS referenced_table,
                ccu.column_name AS referenced_column
            FROM information_schema.key_column_usage kcu
            JOIN information_schema.referential_constraints rc
                ON kcu.constraint_name = rc.constraint_name
            JOIN information_schema.constraint_column_usage ccu
                ON rc.unique_constraint_name = ccu.constraint_name
        ) AS fk
            ON c.column_name = fk.column_name
            AND c.table_name = fk.table_name
            AND c.table_schema = fk.table_schema
        WHERE c.table_schema = '{schemaName}' 
            AND c.table_name = '{tableName}'
        ORDER BY c.ordinal_position;
    ";

            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var column = new Column
                {
                    Name = reader.GetString(0),
                    DataType = reader.GetString(1),
                    IsNotNullable = reader.GetString(2) == "NO",
                    IsPrimaryKey = !reader.IsDBNull(3) && reader.GetBoolean(3)
                };
                columns.Add(column);
            }

            return columns;
        }


    }
}
