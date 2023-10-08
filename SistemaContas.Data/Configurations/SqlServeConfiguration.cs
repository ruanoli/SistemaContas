using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Configurations
{
    public class SqlServeConfiguration
    {
        public static string ConnectionString
            => @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bd_sistema_contas;Integrated Security=True";
        
    }
}
