using Entities.Models.DatabaseCreation;
using Entities.Models.DataTableCreation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatatableRepo
{
    public interface IDatatableRepository
    {
        public Task<DataTable> GetTableSchemaAsync(DataTableService service);

        public Task<bool> CreateTableAsync(DataTableService service);

        public Task UpdateTableAsync(DataTableService service);

        public Task DeleteTableAsync(DataTableService service);
    }
}
