using Entities.Models.DatabaseCreation;
using LoggerService;
using System;
using System.Collections;
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
        public ILoggerManager logger;

        public DatabaseRepository(string databaseName, DatabaseChecker databaseChecker) : base(databaseName)
        {
            dbChecker = databaseChecker;
        }

        public DatabaseRepository(DatabaseChecker databaseChecker, ILoggerManager _logger)
        {
            dbChecker = databaseChecker;
            logger = _logger;
        }

        public DatabaseRepository()
        {

        }

        public async Task<List<Database>> GetDatabasesAsync()
        {
            try
            {
                await OpenConnection();

                List<Database> databases = new();

                var sql = new SqlCommand("SELECT name FROM master.dbo.sysdatabases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')", sqlConnection);

                var reader = await sql.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            databases.Add(new Database(reader[i].ToString()));
                        }
                    }
                }

                return databases;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetDatabasesAsync)} servise method {ex}");
                throw;
            }finally
            {
                await CloseConnection();
            }

          
        }

        public async Task<bool> CreateDatabaseAsync(Database database)
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

        public async Task<bool> DeleteDatabaseAsync(Database database)
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
