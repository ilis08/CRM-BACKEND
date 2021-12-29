using Entities.Models.DatabaseCreation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatabaseRepo
{
    public class DatabaseChecker : DatabaseConfiguration
    {
        public async Task<bool> CheckIfDBExistsAsync(Database database)
        {
            await OpenConnection();

            SqlCommand command = new SqlCommand(ReturnExpression(database.Name), sqlConnection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            object value = default(object);

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

        private static string ReturnExpression(string dbName)
        {
            
            string sql = @$"SELECT CASE WHEN EXISTS (SELECT* FROM master.dbo.sysdatabases WHERE name = '{dbName}')
                        THEN CAST(1 AS BIT) 
                        ELSE CAST(0 AS BIT) END AS DATABASA";

            return sql;
        }

    }
}
