using Dapper;
using SistemaContas.Data.Configurations;
using SistemaContas.Presentations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Repositories
{
    public class CategoryRepository
    {
        public void Insert(Category category)
        {
            var query = @"INSERT INTO CATEGORY(IDCATEGORY, NAME, IDUSER)
                        VALUES(@IdCategory, @Name, @IdUser)";

            using(var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, category);
            }
        }

        public void Update(Category category)
        {
            var query = @"UPDATE CATEGORY
                          SET NAME = @Name
                          WHERE IDCATEGORY = @IdCategory";

            using(var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, category);
            }
        }

        public void Delete(Category category)
        {
            var query = @"DELETE FROM CATEGORY WHERE IDCATEGORY = @IdCategory";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, category);
            }
        }

        public List<Category> GetAllByUser(Guid idUser)
        {
           var query = @"SELECT * FROM CATEGORY WHERE IDUSER = @idUser ORDER BY NAME";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                return connection.Query<Category>(query, new {idUser}).ToList();
            }
        }

        /// <summary>
        /// Retorna as categorias cadastradas ao usuário que está acessando o sistema.
        /// </summary>
        /// <param name="id"></param>
        public Category? GetById(Guid id)
        {
            var query = @"SELECT * FROM CATEGORY WHERE IDCATEGORY = @id";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                return connection.Query<Category>(query, new {id}).FirstOrDefault();
            }
        }

        /// <summary>
        /// Retorna  quantidade de contas cadastradas que estejam vinculadas a categoria de id tal.
        /// </summary>
        /// <param name="idCategory"></param>
        public int? GetQuantityBill(Guid idCategory)
        {
            var query = @"SELECT COUNT(*) FROM BILL WHERE IDCATEGORY = @idCategory";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                return connection.Query<int>(query, new { idCategory }).FirstOrDefault();
            }
        }
    }
}
