using Dados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace Dados
{
    public class Contexto : IContexto
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["TesteMVC"].ToString();

        public List<Usuario> GetUser()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                var result = sqlConnection.Query<Usuario>("Select * from Usuario");

                foreach (Usuario usuario in result)
                    usuarios.Add(usuario);
            }

            return usuarios;
        }

        public Usuario GetUserById(int userID)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<Usuario>("Select * from Usuario where IdUsuario = @IdUsuario", new { IdUsuario = userID }).SingleOrDefault();
            }
        }

        public int SaveUser(Usuario model)
        {
            int idCreated = 0;

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                idCreated = sqlConnection.Query<int>(@"INSERT INTO Usuario(NomeCompleto,Email, telefone) VALUES (@NomeCompleto, @Email, @telefone);
                    SELECT CAST(SCOPE_IDENTITY() as int);", model).Single();
            }

            return idCreated;
        }

        public void UpdateUser(Usuario model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute(@"UPDATE Usuario
                                       SET NomeCompleto = @NomeCompleto, 
                                           Email = @Email,
                                           telefone = @telefone
                                       WHERE IdUsuario = @IdUsuario", model);
            }
        }

        public void DeleteUser(int IdUsuario)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute($"DELETE FROM Usuario WHERE IdUsuario = {IdUsuario}");
            }
        }
    }
}
