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
            var query = @"INSERT INTO BILL(IDBILL, NAME, DATE, VALUE, TYPE, OBSERVATION, IDCATEGORY, IDUSER)
                        VALUES(@IdBill, @Name, @DataBill, @ValueBill, @TypeBill, @Comments, @IdCategory, @IdUser)";

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

        public void Delete(Bill bill)
        {
            var query = @"DELETE FROM BILL 
                        WHERE IDBILL = @IdBill";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, bill);
            }
        }

        //JOIN com outra tabela utilizando o dapper.
        //splitOn é para pegarmos a foreign key
        public Bill? GetBillById(Guid id)
        {
            var query = @"SELECT * FROM BILL AS conta
                        INNER JOIN CATEGORY AS cate 
                        ON conta.IDCATEGORY = cate.IDCATEGORY
                        WHERE conta.IDBILL = @id";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                return connection.Query(query, 
                    (Bill bill, Category category) =>
                    {
                        bill.Category = category;
                        return bill;
                    },
                    new { id },
                    splitOn: "IdCategory")
                    .FirstOrDefault();
            }
        }

        public IList<Bill> GetBillAll(DateTime? startDate, DateTime? endDate, Guid idUser)
        {
            var query = @"SELECT * FROM BILL AS a
                        INNER JOIN CATEGORY AS b
                        ON a.IDCATEGORY = b.IDCATEGORY
                        WHERE DATE BETWEEN @startDate AND @endDate AND a.IDUSER = @idUser
                        ORDER BY DATE DESC";

            //Exemplo de join com dapper.
            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                return connection.Query(query,
                    (Bill bill, Category category) =>
                    {
                        bill.Category = category;
                        return bill;
                    },
                    new {startDate, endDate, idUser},
                    splitOn: "IdCategory")
                    .ToList();
            }
        }

    }
}
