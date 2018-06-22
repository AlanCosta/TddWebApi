using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public string telefone { get; set; }
    }
}
