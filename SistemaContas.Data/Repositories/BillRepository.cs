using Dapper;
using SistemaContas.Data.Configurations;
using SistemaContas.Data.Entities;
using SistemaContas.Presentations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Repositories
{
    public class BillRepository
    {
        public void Insert(Bill bill)
        {
            var query = @"INSERT INTO CATEGORY(IDBILL, NAME, DATE, VALUE, TYPE, OBSERVATION, IDCATEGORY, IDUSER)
                        VALUES(@IIdBill, @Name, @DataBill, @ValueBill, @TypeBill, @Comments, @IdCategory, @IdUser)";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, bill);
            }
        }

        public void Update(Bill bill)
        {
            var query = @"UPDATE BILL 
                        SET IDBILL = @IIdBill, NAME = @Name, DATE = @DataBill, 
                        VALUE = @ValueBill, TYPE = @TypeBill, OBSERVATION = @Comments, 
                        IDCATEGORY = @IdCategory, IDUSER = @IdUser
                        WHERE IDBILL = @IdBill";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, bill);
            }
        }
    }
}
