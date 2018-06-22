using AutoMapper;
using CrudWebApi.Validator;
using Dados;
using Dados.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudWebApi.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private readonly Contexto _contexto;

        public UsuarioController()
        {
            _contexto = new Contexto();
        }

        [HttpGet]
        [Route("all")]
        public List<Usuario> GetUsers()
        {
            return _contexto.GetUser();
        }

        [HttpGet]
        [Route("{id}")]
        public Usuario GetById(int id)
        {
            return _contexto.GetUserById(id);
        }

        [HttpPut]
        [Route("update")]
        public IHttpActionResult UpdateUser([FromBody]UsuarioDto model)
        {
            try
            {
                UsuarioValidator validator = new UsuarioValidator();
                ValidationResult results = validator.Validate(model);
                if (results.IsValid)
                {
                    var usuarioDomain = Mapper.Map<UsuarioDto, Usuario>(model);
                    _contexto.UpdateUser(usuarioDomain);
                    return Ok();

                } else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                _contexto.DeleteUser(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        [Route("save")]
        public IHttpActionResult SaveUser([FromBody]UsuarioDto model)
        {
            try
            {
                UsuarioValidator validator = new UsuarioValidator();
                ValidationResult results = validator.Validate(model);
                if (results.IsValid)
                {
                    var usuarioDomain = Mapper.Map<UsuarioDto, Usuario>(model);

                    int idInserted = _contexto.SaveUser(usuarioDomain);

                    return Created("save", idInserted);
                }
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
