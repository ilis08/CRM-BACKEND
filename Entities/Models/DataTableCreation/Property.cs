using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.DataTableCreation
{
    public class Property
    {
        public string FieldName { get; set; }

        public SqlDbType DataType { get; set; }

        public bool IsNullable { get; set; }

        public int Size { get; set; }

        public string Constraint { get; set; }
    }
}
