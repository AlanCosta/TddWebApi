using Dados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public interface IContexto
    {
        List<Usuario> GetUser();

        Usuario GetUserById(int userID);

        int SaveUser(Usuario model);

        void UpdateUser(Usuario model);

        void DeleteUser(int IdUsuario);
    }
}
