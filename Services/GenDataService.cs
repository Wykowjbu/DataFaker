

using Bogus;
using DataFaker.Models;
using System.Collections.ObjectModel;

namespace DataFaker.Services
{
    public class GenDataService
    {
        public GenDataService()
        {
        }

        public object GetData(ColumnPlus columnPlus)
        {
            if (columnPlus.IsLocked)
            {
                return columnPlus.Value;
            }
            var faker = new Faker();

            if (columnPlus.IsPrimaryKey)
            {
                if (columnPlus.IsAutoIncrement)
                {
                    return "Auto Increment";
                }
                return faker.IndexFaker + 1;
            }

            return GenerateValue(faker, columnPlus.DataType);
        }

        private object GenerateValue(Faker f,string dataType)
        {
            switch (dataType.ToLower())
            {
                case "integer":
                case "int":
                case "bigint":
                case "smallint":
                    return f.Random.Int();
                case "real":
                case "double precision":
                case "numeric":
                case "decimal":
                    return f.Random.Double();
                case "boolean":
                case "bool":
                    return f.Random.Bool();
                case "character varying":
                case "varchar":
                case "text":
                    return f.Lorem.Word();
                case "date":
                    return f.Date.Past().Date;
                case "timestamp":
                case "timestamp without time zone":
                    return f.Date.Past();
                default:
                    return null;
            }
        }


    }
}
