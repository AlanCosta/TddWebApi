using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudWebApi.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        public int NomeCompleto { get; set; }

        [Required]
        [EmailAddress]
        public int Email { get; set; }

        [Required]
        public int telefone { get; set; }

    }
}