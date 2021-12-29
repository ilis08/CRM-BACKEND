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
        public Task<DataTable> GetTableSchema(DataTableService service);

        public Task<bool> CreateTable(DataTableService service);

        public Task UpdateTable(DataTableService service);

        public Task DeleteTable(DataTableService service);
    }
}
