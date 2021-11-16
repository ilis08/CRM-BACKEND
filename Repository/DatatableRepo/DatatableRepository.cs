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

        public async Task CreateTable(DataTableService service)
        {
            await OpenConnection(service.Database);

            var tables = string.Join(",", service.Table.Properties.Select(c => string.Format($"{c.FieldName} {c.DataType}")));

            var sql = new SqlCommand($"CREATE TABLE {service.Table.Name} ( {tables} );", sqlConnection);

            sql.CommandType = CommandType.Text;
            await sql.ExecuteNonQueryAsync();

            await CloseConnection();
        }

        public async Task DeleteTable(DataTableService service)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTable(DataTableService service)
        {
            throw new NotImplementedException();
        }
    }
}
