using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Connection
    {
        public SqlConnection MyConnection()
        {
            SqlConnection sql = new SqlConnection("Data Source=.;Initial Catalog=SMG ;Integrated Security=SSPI");

            return sql;
        }
    }
}
