using AutoMapper;
using LoggerService;
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
        private readonly ILoggerManager logger;
        private readonly DatabaseChecker databaseChecker;
        private readonly DatatableChecker datatableChecker;
        private readonly IMapper mapper;


        public RepositoryManager(ILoggerManager _logger, DatatableChecker _datatableChecker, DatabaseChecker _databaseChecker, IMapper _mapper)
        {
            logger = _logger;
            datatableChecker = _datatableChecker;
            databaseChecker = _databaseChecker;
            mapper = _mapper;   
        }

        private IDatabaseRepository _databaseRepository;
        private IDatatableRepository _datatableRepository;

        public IDatabaseRepository Database
        {
            get
            {
                if (_databaseRepository == null)
                {
                    _databaseRepository = new DatabaseRepository(databaseChecker, logger);
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
                    _datatableRepository = new DatatableRepository(datatableChecker, logger, mapper);
                }

                return _datatableRepository;
            }
        }
    }
}
