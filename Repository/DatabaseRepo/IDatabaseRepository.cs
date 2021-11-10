using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatabaseRepo
{
    public interface IDatabaseRepository
    {
        public Task CreateDatabase(string name);

        public Task DeleteDatabase(string name);
    }
}
