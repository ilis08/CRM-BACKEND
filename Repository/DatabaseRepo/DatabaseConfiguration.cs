using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repository.DatabaseRepo
{
    public class DatabaseConfiguration
    {
        private readonly string connectionString;
        protected SqlConnection sqlConnection = null;

        public DatabaseConfiguration() : this("Server = (localdb)\\MSSQLLocalDB;Database=master;MultipleActiveResultSets=True")
        {

        }

        public DatabaseConfiguration(string _connectionString)
        {
            connectionString = _connectionString;
        }

        protected async Task OpenConnection()
        {   
            sqlConnection = new()
            {
                ConnectionString = connectionString
            };

            await sqlConnection.OpenAsync();
        }

        protected async Task OpenConnection(Entities.Models.DatabaseCreation.Database database)
        {
            sqlConnection = new()
            {
                ConnectionString = ReturnConnectionString(database)
            };

            await sqlConnection.OpenAsync();
        }

        protected async Task CloseConnection()
        {
            if (sqlConnection?.State != ConnectionState.Closed)
            {
                await sqlConnection?.CloseAsync();
            }
        }

        private string ReturnConnectionString(Entities.Models.DatabaseCreation.Database database)
        {
            string connection = $"Server = (localdb)\\MSSQLLocalDB;Database={database.Name};MultipleActiveResultSets=True";

            return connection;
        }
    }
}
