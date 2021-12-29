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
        private DatatableChecker datatableChecker;

        public DatatableRepository(DatatableChecker _datatableChecker)
        {
            datatableChecker = _datatableChecker;
        }

        public DatatableRepository()
        {

        }

        public async Task<bool> CreateTable(DataTableService service)
        {
            await OpenConnection(service.Database);

            var isTableExisted = await datatableChecker.CheckIfTableExistInDBAsync(service);

            if (!isTableExisted)
            {
                try
                {
                    var tables = string.Join(",", service.Table.Properties.Select(c => string.Format($"{c.FieldName} {c.DataType}")));

                    var sql = new SqlCommand($"CREATE TABLE {service.Table.Name} ( {tables} );", sqlConnection);

                    sql.CommandType = CommandType.Text;
                    await sql.ExecuteNonQueryAsync();

                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }finally
                {
                    await CloseConnection();
                }
       
            }
            else
            {
                await CloseConnection();
                return false;
            }
        }

        public async Task DeleteTable(DataTableService service)
        {
            await OpenConnection(service.Database);

            var sql = new SqlCommand($"DROP TABLE @tableName");

            var tableName = new SqlParameter("@tableName", SqlDbType.NVarChar, 10);
            tableName.Value = service.Table.Name;

            sql.Parameters.Add(tableName);

            sql.CommandType = CommandType.Text;

            await sql.ExecuteNonQueryAsync();

            await CloseConnection();
        }



        public async Task<DataTable> GetTableSchema(DataTableService service)
        {
            await OpenConnection(service.Database);

            SqlCommand command = new SqlCommand(String.Format("SELECT * FROM {0}","[" + service.Table.Name.Replace("]", "]]") + "]"), sqlConnection);

            var reader = await command.ExecuteReaderAsync();

            var sql = await reader.GetSchemaTableAsync();

            await CloseConnection();
            return sql;
        }

        public async Task UpdateTable(DataTableService service)
        {
            throw new NotImplementedException();
        }
    }
}
