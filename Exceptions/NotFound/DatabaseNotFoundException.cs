using Entities.Models.DatabaseCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.NotFound
{
    public sealed class DatabaseNotFoundException : NotFoundException
    {
        public DatabaseNotFoundException(Database database) : base($"The database : {database.Name} doesn't exist in the server.")
        {
        }
    }
}
