using Dapper;
using SistemaContas.Data.Configurations;
using SistemaContas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Repositories
{
    public class UserRepository
    {
        //Encripta uma senha no SQL Server usando o algoritmo MD5:
        //CONVERT(VARCHAR(32), HASHBYTES('MD5', @Password),2)
        public void Insert(UserRegister user)
        {
            var query = @"INSERT INTO USERREGISTER(
                            IDUSER,
                            NAME,
                            PASSWORD,
                            EMAIL,  
                            DATETIMECREATION)
                        VALUES(
                            @IdUser,
                            @Name,
                            CONVERT(VARCHAR(32), HASHBYTES('MD5', @Password),2),
                            @Email,
                            @DateTimeCreation)";

            using(var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, user);
            }
        }

        public void Update(UserRegister user)
        {
            var query = @"UPDATE USERREGISTER 
                          SET NAME = @Name, 
                              EMAIL = @Email,
                              PASSWORD = @Password, 
                              DATETIMECREATION = @DateTimeCreation
                          WHERE IDUSER = @IdUser";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, user);
            }
        }

        public void Delete(UserRegister user)
        {
            var query = @"DELETE * FROM USERREGISTER WHERE IDUSER = @IdUser";

            using (var connection = new SqlConnection(SqlServeConfiguration.ConnectionString))
            {
                connection.Execute(query, user);
            }
        }
    }
}
