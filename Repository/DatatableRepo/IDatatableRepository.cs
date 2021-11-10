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
        public Task CreateTable(Database database,Table table);

        public Task UpdateTable(Database database, Table table);

        public Task DeleteTable(Database database, Table table);
    }
}
