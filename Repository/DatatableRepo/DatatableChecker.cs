using Entities.Models.DataTableCreation;
using Repository.DatabaseRepo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatatableRepo
{
    public class DatatableChecker : DatabaseConfiguration
    {
        private DatabaseChecker dbChecker;

        public DatatableChecker(DatabaseChecker databaseChecker)
        {
            dbChecker = databaseChecker;
        }

        public async Task<bool> CheckIfTableExistInDBAsync(DataTableService table)
        {
            await OpenConnection();

            var command = ReturnExpression(table);

            command.Connection = sqlConnection;

            var reader = await command.ExecuteReaderAsync();

            var value = default(object);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    value = reader.GetValue(0);
                }
            }

            await CloseConnection();

            if ((bool)value == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static SqlCommand ReturnExpression(DataTableService table)
        {
            var command = new SqlCommand(string.Format("USE {0}; SELECT CASE WHEN EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName) " +
                    "THEN CAST(1 AS BIT) " +
                        "ELSE CAST(0 AS BIT) END AS DATABASA","[" + table.Database.Name + "]", table.Table));

            var tableName = new SqlParameter("@tableName", table.Table.Name);

            command.Parameters.Add(tableName);

            return command;
        }
    }
}
