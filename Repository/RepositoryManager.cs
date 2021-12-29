using Repository.DatabaseRepo;
using Repository.DatatableRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private IDatabaseRepository _databaseRepository;
        private IDatatableRepository _datatableRepository;

        public IDatabaseRepository Database
        {
            get
            {
                if (_databaseRepository == null)
                {
                    _databaseRepository = new DatabaseRepository(new DatabaseChecker());
                }

                return _databaseRepository;
            }
        }

        public IDatatableRepository Datatable
        {
            get
            {
                if (_datatableRepository == null)
                {
                    _datatableRepository = new DatatableRepository(new DatatableChecker(new DatabaseChecker()));
                }

                return _datatableRepository;
            }
        }
    }
}
