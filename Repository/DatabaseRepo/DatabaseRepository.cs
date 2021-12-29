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
        public DatabaseChecker dbChecker;

        public DatabaseRepository(string databaseName, DatabaseChecker databaseChecker) : base(databaseName)
        {
            dbChecker = databaseChecker;
        }

        public DatabaseRepository(DatabaseChecker databaseChecker)
        {
            dbChecker = databaseChecker;
        }

        public DatabaseRepository()
        {

        }

        public async Task<bool> CreateDatabase(Database database)
        {
            await OpenConnection();

            var value = await dbChecker.CheckIfDBExistsAsync(database);

            if (value != true)
            {
                var sql = new SqlCommand($"CREATE DATABASE {database.Name}", sqlConnection);

                sql.CommandType = CommandType.Text;
                sql.ExecuteNonQuery();

                await CloseConnection();
                return true;
            }
            else
            {
                await CloseConnection();
                return false;
            }
        }

        public async Task<bool> DeleteDatabase(Database database)
        {
            await OpenConnection();

            var value = await dbChecker.CheckIfDBExistsAsync(database);

            if (value != true)
            {

                var sql = new SqlCommand($"DROP DATABASE {database.Name}", sqlConnection);

                sql.CommandType = CommandType.Text;
                sql.ExecuteNonQuery();

                await CloseConnection();

                return true;
            }
            else
            {
                await CloseConnection();

                return false;
            }
        }


    }
}
