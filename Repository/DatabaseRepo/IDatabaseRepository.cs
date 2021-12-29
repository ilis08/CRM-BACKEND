using Entities.Models.DatabaseCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatabaseRepo
{
    public interface IDatabaseRepository
    {
        public Task<bool> CreateDatabase(Database database);

        public Task<bool> DeleteDatabase(Database database);
    }
}
