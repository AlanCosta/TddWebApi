using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dados;
using System.Collections.Generic;
using Dados.Models;
using Moq;
using System.Linq;

namespace ContextoTest
{
    [TestClass]
    public class ContextoTest
    {
        private readonly Contexto _contexto;
        private readonly IContexto _iContexto;

        public ContextoTest()
        {
            _contexto = new Contexto();
        }

        [TestMethod]
        public void SaveUser()
        {
            List<Usuario> usuarios = new List<Usuario>
                {
                new Usuario { IdUsuario = 1, NomeCompleto = "C# Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 2, NomeCompleto = "ASP.Net Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 3, NomeCompleto = "Silverlight Unleashed",
                        Email = "Short description here", telefone = "985592335" }
                };


            Mock<IContexto> mockUserRepository = new Mock<IContexto>();

            mockUserRepository.Setup(mr => mr.SaveUser(It.IsAny<Usuario>())).Returns(
                (Usuario usuario) =>
                {
                    DateTime now = DateTime.Now;

                    if (usuario.IdUsuario.Equals(default(int)))
                    {
                        usuario.NomeCompleto = "teste " + usuarios.Count() + 1;
                        usuario.Email = "Teste@teste.com";
                        usuario.IdUsuario = usuarios.Count() + 1;
                        usuario.telefone = "985592335";
                        usuarios.Add(usuario);
                    }
                    else
                    {
                        var original = usuarios.Where(q => q.IdUsuario == usuario.IdUsuario).Single();

                        if (original == null)
                        {
                            return 0;
                        }

                        original.NomeCompleto = usuario.NomeCompleto;
                        original.Email = usuario.Email;
                        original.telefone = usuario.telefone;
                    }

                    return 1;
                });

            // Return all the products
            mockUserRepository.Setup(mr => mr.GetUser()).Returns(usuarios);

            Usuario user = new Usuario { NomeCompleto = "Testeando", Email = "Teste@Tteste.com", telefone = "98559-2335" };

            var userCount = mockUserRepository.Object.GetUser().Count;

            Assert.AreEqual(3, userCount);

            var userId = mockUserRepository.Object.SaveUser(user);
            userCount = mockUserRepository.Object.GetUser().Count;

            
            Assert.AreEqual(4, userCount);
            Assert.IsNotNull(userId);
        }

        [TestMethod]
        public void UpdateUser()
        {
            List<Usuario> usuarios = new List<Usuario>
                {
                new Usuario { IdUsuario = 1, NomeCompleto = "C# Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 2, NomeCompleto = "ASP.Net Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 3, NomeCompleto = "Silverlight Unleashed",
                        Email = "Short description here", telefone = "985592335" }
                };


            Mock<IContexto> mockUserRepository = new Mock<IContexto>();

            mockUserRepository.Setup(mr => mr.UpdateUser(It.IsAny<Usuario>()));

            mockUserRepository.Setup(mr => mr.GetUserById(It.IsAny<int>())).Returns((int i) => usuarios.Where(x => x.IdUsuario == i).Single());

            Usuario user = new Usuario { IdUsuario = 1, NomeCompleto = "Testeando", Email = "Teste@Tteste.com", telefone = "98559-2335" };

            var userById = mockUserRepository.Object.GetUserById(1);

            Assert.AreEqual("C# Unleashed", userById.NomeCompleto);

            mockUserRepository.Object.UpdateUser(user);
            userById = mockUserRepository.Object.GetUserById(1);

            Assert.AreEqual("Testeando", userById.NomeCompleto);
        }

        [TestMethod]
        public void SelectUsers()
        {
            List<Usuario> usuarios = new List<Usuario>
                {
                new Usuario { IdUsuario = 1, NomeCompleto = "C# Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 2, NomeCompleto = "ASP.Net Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 3, NomeCompleto = "Silverlight Unleashed",
                        Email = "Short description here", telefone = "985592335" }
                };

            // Mock the Products Repository using Moq
            Mock<IContexto> mockUserRepository = new Mock<IContexto>();

            // Return all the products
            mockUserRepository.Setup(mr => mr.GetUser()).Returns(usuarios);

            var users = mockUserRepository.Object.GetUser();

            Assert.IsNotNull(users); // Test if null
            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public void SelectUserById()
        {
            IList<Usuario> usuarios = new List<Usuario>
                {
                new Usuario { IdUsuario = 1, NomeCompleto = "C# Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 2, NomeCompleto = "ASP.Net Unleashed",
                        Email = "Short description here", telefone = "985592335" },
                    new Usuario { IdUsuario = 3, NomeCompleto = "Silverlight Unleashed",
                        Email = "Short description here", telefone = "985592335" }
                };

            // Mock the Products Repository using Moq
            Mock<IContexto> mockUserRepository = new Mock<IContexto>();

            // Return all the products
            mockUserRepository.Setup(mr => mr.GetUserById(It.IsAny<int>())).Returns((int i) => usuarios.Where(x => x.IdUsuario == i).Single());

            var user = mockUserRepository.Object.GetUserById(1);

            Assert.IsNotNull(user); // Test if null
            Assert.AreEqual("C# Unleashed", user.NomeCompleto);
        }

    }
}
