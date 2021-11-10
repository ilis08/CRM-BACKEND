
using Repository.DatabaseRepo;
using Repository.DatatableRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepositoryManager
    {
        public IDatabaseRepository Database { get; }
        public IDatatableRepository Datatable { get; }
    }
}
