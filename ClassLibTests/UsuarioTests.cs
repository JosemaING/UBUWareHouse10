using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLib;
using UBULib;
using System;

namespace ClassLib.Tests
{
    [TestClass]
    public class UsuarioTests
    {
        [TestMethod]
        public void Constructor_AsignacionCorrecta()
        {
            // Arrange
            string nombre = "Juan";
            string apellidos = "Pérez";
            string email = "juan.perez@example.com";
            string password = "SecurePass123!";
            bool cuentagratuita = true;

            // Act
            Usuario usuario = new Usuario(nombre, apellidos, email, password, cuentagratuita);

            // Assert
            Assert.AreEqual(nombre, usuario.Nombre);
            Assert.AreEqual(apellidos, usuario.Apellidos);
            Assert.AreEqual(email, usuario.Email);
            Assert.AreEqual(cuentagratuita, usuario.Cuentagratuita);
            Assert.IsTrue(usuario.Activo);
            Assert.IsFalse(usuario.EsGestor);
            Assert.IsNotNull(usuario.Password); // Comprobamos que la contraseña ha sido encriptada y no es null
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NombreNull_LanzaExcepcion()
        {
            // Arrange
            string nombre = null;
            string apellidos = "Pérez";
            string email = "juan.perez@example.com";
            string password = "SecurePass123!";
            bool cuentagratuita = true;

            // Act
            Usuario usuario = new Usuario(nombre, apellidos, email, password, cuentagratuita);
        }

        [TestMethod]
        public void CompruebaPassword_ContrasenaCorrecta_DevuelveTrue()
        {
            // Arrange
            string password = "SecurePass123!";
            Usuario usuario = new Usuario("Juan", "Pérez", "juan.perez@example.com", password, true);

            // Act
            bool resultado = usuario.CompruebaPassword(password);

            // Assert
            Assert.IsTrue(resultado);
            Assert.AreNotEqual(DateTime.MinValue, usuario.UltimoInicioSesion); // Verifica que la fecha de último inicio se ha actualizado
        }

        [TestMethod]
        public void CompruebaPassword_ContrasenaIncorrecta_DevuelveFalse()
        {
            // Arrange
            string password = "SecurePass123!";
            Usuario usuario = new Usuario("Juan", "Pérez", "juan.perez@example.com", password, true);
            string incorrectPassword = "WrongPass456!";

            // Act
            bool resultado = usuario.CompruebaPassword(incorrectPassword);

            // Assert
            Assert.IsFalse(resultado);
            Assert.AreEqual(DateTime.MinValue, usuario.UltimoInicioSesion); // Verifica que la fecha de último inicio no se ha actualizado
        }
    }
}
