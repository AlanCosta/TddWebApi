using Dados.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWebApi.Validator
{
    public class UsuarioValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioValidator()
        {
            RuleFor(user => user.NomeCompleto).NotEmpty().WithMessage("Please specify a Full Name");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Please specify a E-mail");
            RuleFor(user => user.Telefone).NotEmpty().WithMessage("Please specify a Phone");
        }
    }
}