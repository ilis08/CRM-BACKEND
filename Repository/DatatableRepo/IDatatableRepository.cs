using Entities.Models.DatabaseCreation;
using Entities.Models.DataTableCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatatableRepo
{
    public interface IDatatableRepository
    {
        public Task CreateTable(DataTableService service);

        public Task UpdateTable(DataTableService service);

        public Task DeleteTable(DataTableService service);
    }
}
