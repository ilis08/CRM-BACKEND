using Entities.Models.DatabaseCreation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatabaseRepo
{
    public class DatabaseRepository : DatabaseConfiguration, IDatabaseRepository
    {
        public DatabaseRepository(string databaseName) : base(databaseName)
        {

        }

        public DatabaseRepository()
        {

        }

        public async Task<bool> CreateDatabase(Database database)
        {
            await OpenConnection();

            DatabaseChecker checker = new();

            var value = await checker.CheckIfDBExistsAsync(database);

            if (value != true)
            {
                var sql = new SqlCommand($"CREATE DATABASE {database.Name}", sqlConnection);

                sql.CommandType = CommandType.Text;
                sql.ExecuteNonQuery();

                return true;
            }
            else
            {
                await CloseConnection();
                return false;
            }
        }

        public async Task DeleteDatabase(string name)
        {
            await OpenConnection();

            var sql = new SqlCommand($"DROP DATABASE {name}", sqlConnection);

            sql.CommandType = CommandType.Text;
            sql.ExecuteNonQuery();

            await CloseConnection();
        }


    }
}
