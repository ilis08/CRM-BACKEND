using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.DataTableCreation
{
    public class Property
    {
        public string Name { get; set; }

        public SqlDbType DataType { get; set; }

        public string Constraint { get; set; }
    }
}
