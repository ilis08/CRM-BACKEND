using Entities.Models.DataTableCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.NotFound
{
    public sealed class DatatableNotFoundException : NotFoundException
    {
        public DatatableNotFoundException(DataTableService dataTable) : base($"The table : {dataTable.Table.Name} doesn't exist in the {dataTable.Database.Name} database")
        {
        }
    }
}
