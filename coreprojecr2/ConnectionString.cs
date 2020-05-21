using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreprojecr2
{
    public class ConnectionString
    {
        private static string cName = "Data Source=VINODS-PDG\\SQLEXPRESS;  Initial Catalog = sms2; User ID = sa; Password=abc123";
        
        public static  string CName => cName;
    }
}
