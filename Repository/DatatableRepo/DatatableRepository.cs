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
using LoggerService;
using AutoMapper;
using Exceptions.NotFound;
using System.ComponentModel.DataAnnotations;

namespace Repository.DatatableRepo
{
    public class DatatableRepository : DatabaseConfiguration, IDatatableRepository
    {
        private readonly DatatableChecker datatableChecker;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public DatatableRepository(DatatableChecker _datatableChecker, ILoggerManager _logger, IMapper _mapper)
        {
            datatableChecker = _datatableChecker;
            logger = _logger;
            mapper = _mapper;
        }

        public DatatableRepository()
        {

        }

        public async Task<bool> CreateTableAsync(DataTableService service)
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

        public async Task DeleteTableAsync(DataTableService service)
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



        public async Task<DataTable> GetTableSchemaAsync(DataTableService service)
        {
            try
            {
                await OpenConnection(service.Database);

                var isTableExisted = await datatableChecker.CheckIfTableExistInDBAsync(service);

                if (isTableExisted == false)
                {
                    throw new DatatableNotFoundException(service);
                }

                SqlCommand command = new SqlCommand(String.Format("SELECT * FROM {0}", "[" + service.Table.Name.Replace("]", "]]") + "]"), sqlConnection);

                var reader = await command.ExecuteReaderAsync();

                DataTable schema = new();

                schema = await reader.GetSchemaTableAsync();

                DataTableService dataTable = new DataTableService()
                {
                    Table = new Table()
                    {
                        Properties = new List<Property>()
                    }
                };

                dataTable.Database = service.Database;

                List<Property> properties = new();

                if (schema is not null)
                {
                    foreach (DataRow row in schema.Rows)
                    {
                        foreach (DataColumn column in schema.Columns)
                        {
                            var dColumn = row[column];

                            var property = mapper.Map<Property>(dColumn);

                            properties.Add(property);
                        }
                    }

                    dataTable.Table.Properties.AddRange(properties);
                }
                await reader.CloseAsync();

                return schema; 
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetTableSchemaAsync)} servise method {ex}");
                throw;
            }
            finally
            {
                await CloseConnection();
            }
        }

        public async Task UpdateTableAsync(DataTableService service)
        {
            throw new NotImplementedException();
        }
    }
}
