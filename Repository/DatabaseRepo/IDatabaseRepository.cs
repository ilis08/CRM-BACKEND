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
        public Task<List<Database>> GetDatabasesAsync();

        public Task<bool> CreateDatabaseAsync(Database database);

        public Task<bool> DeleteDatabaseAsync(Database database);
    }
}
