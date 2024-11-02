using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLib;
using UBUWHdb;
using System.Collections.Generic;

namespace UBUWHdb.Tests
{
    [TestClass]
    public class WHdbTests
    {
        private WHdb db;

        [TestInitialize]
        public void TestInitialize()
        {
            db = new WHdb();
        }

        [TestMethod]
        public void GuardaUsuario_NuevoUsuario_DevuelveTrue()
        {
            // Arrange
            Usuario usuario = new Usuario("Ana", "García", "ana@example.com", "password", false);

            // Act
            bool resultado = db.GuardaUsuario(usuario);

            // Assert
            Assert.IsTrue(resultado);
            Assert.IsNotNull(db.LeeUsuario("ana@example.com"));
        }

        [TestMethod]
        public void LeeUsuario_EmailExistente_DevuelveUsuario()
        {
            // Arrange
            string email = "iml1012@alu.ubu.es";

            // Act
            Usuario usuario = db.LeeUsuario(email);

            // Assert
            Assert.IsNotNull(usuario);
            Assert.AreEqual(email, usuario.Email);
        }

        [TestMethod]
        public void LeeUsuario_EmailNoExistente_DevuelveNull()
        {
            // Act
            Usuario usuario = db.LeeUsuario("noexiste@example.com");

            // Assert
            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void ValidaUsuario_ContrasenaCorrecta_DevuelveTrue()
        {
            // Arrange
            string email = "iml1012@alu.ubu.es";
            string password = "Estudiante1";

            // Act
            bool resultado = db.ValidaUsuario(email, password);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ValidaUsuario_ContrasenaIncorrecta_DevuelveFalse()
        {
            // Arrange
            string email = "iml1012@alu.ubu.es";
            string password = "ContraseñaIncorrecta";

            // Act
            bool resultado = db.ValidaUsuario(email, password);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void GuardaComponente_NuevoComponente_DevuelveTrue()
        {
            // Arrange
            Componente componente = new Componente(0, "Componente 1", "Descripción del componente");

            // Act
            bool resultado = db.GuardaComponente(componente);

            // Assert
            Assert.IsTrue(resultado);
            Assert.AreEqual(1, db.ObtenerNumeroDeComponentes(0));
        }

        [TestMethod]
        public void LeeComponente_ComponenteExistente_DevuelveComponente()
        {
            // Arrange
            Componente componente = new Componente(0, "Componente 1", "Descripción del componente");
            db.GuardaComponente(componente);

            // Act
            Componente obtenido = db.LeeComponente(componente.IdElemento);

            // Assert
            Assert.IsNotNull(obtenido);
            Assert.AreEqual(componente.IdElemento, obtenido.IdElemento);
        }

        [TestMethod]
        public void CambiaPassword_UsuarioExistente_DevuelveTrue()
        {
            // Arrange
            int idUsuario = 0;
            string nuevaPassword = "NuevaPassword123";

            // Act
            bool resultado = db.CambiaPassword(idUsuario, nuevaPassword);

            // Assert
            Assert.IsTrue(resultado);
            Assert.AreEqual(nuevaPassword, db.LeeUsuarioPorId(idUsuario).Password);
        }

        [TestMethod]
        public void DesactivaUsuario_UsuarioExistente_DevuelveTrue()
        {
            // Arrange
            int idUsuario = 1;

            // Act
            bool resultado = db.DesactivaUsuario(idUsuario);

            // Assert
            Assert.IsTrue(resultado);
            Assert.IsFalse(db.LeeUsuarioPorId(idUsuario).Activo);
        }
    }
}
