using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.DataTableCreation
{
    public class Table
    {
        public string Name { get; set; }

        public List<SqlParameter> Properties { get; set; }
    }
}
