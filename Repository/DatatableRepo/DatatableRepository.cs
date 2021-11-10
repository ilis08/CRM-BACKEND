using Entities.Models.DataTableCreation;
using Repository.DatabaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.DatabaseCreation;
using System.Data.SqlClient;
using System.Data;

namespace Repository.DatatableRepo
{
    public class DatatableRepository : DatabaseConfiguration, IDatatableRepository
    {
        public DatatableRepository()
        {

        }

        public async Task CreateTable(Database database,Table table)
        {
            await OpenConnection(database);

            var tables = string.Join(",", table.Properties.Select(c => string.Format($"{c.ParameterName} {c.SqlDbType}")));

            var sql = new SqlCommand($"CREATE TABLE {table.Name} ( {tables} );", sqlConnection);

            sql.CommandType = CommandType.Text;
            await sql.ExecuteNonQueryAsync();

            await CloseConnection();

        }

        public async Task DeleteTable(Database database, Table table)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTable(Database database, Table table)
        {
            throw new NotImplementedException();
        }
    }
}
