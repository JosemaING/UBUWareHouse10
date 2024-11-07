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
            // Arrange - Define los valores iniciales para crear un usuario
            string nombre = "Juan";
            string apellidos = "Pérez";
            string email = "juan.perez@example.com";
            string password = "SecurePass123!";
            bool cuentagratuita = true;

            // Act - Crea una instancia de Usuario con los valores iniciales
            Usuario usuario = new Usuario(nombre, apellidos, email, password, cuentagratuita);

            // Assert - Verifica que las propiedades se asignaron correctamente
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
            // Arrange - Define un nombre nulo para provocar una excepción en el constructor
            string nombre = null;
            string apellidos = "Pérez";
            string email = "juan.perez@example.com";
            string password = "SecurePass123!";
            bool cuentagratuita = true;

            // Act - Intenta crear un usuario con un nombre nulo, lo cual debe lanzar una excepción
            Usuario usuario = new Usuario(nombre, apellidos, email, password, cuentagratuita);
        }

        [TestMethod]
        public void CompruebaPassword_ContrasenaCorrecta_DevuelveTrue()
        {
            // Arrange - Crea un usuario con una contraseña específica
            string password = "SecurePass123!";
            Usuario usuario = new Usuario("Juan", "Pérez", "juan.perez@example.com", password, true);

            // Act - Verifica la contraseña correcta
            bool resultado = usuario.CompruebaPassword(password);

            // Assert - Comprueba que la contraseña es correcta y que la fecha de último inicio se actualizó
            Assert.IsTrue(resultado);
            Assert.AreNotEqual(DateTime.MinValue, usuario.UltimoInicioSesion); // Verifica que la fecha de último inicio se ha actualizado
        }

        [TestMethod]
        public void CompruebaPassword_ContrasenaIncorrecta_DevuelveFalse()
        {
            // Arrange - Crea un usuario con una contraseña específica
            string password = "SecurePass123!";
            Usuario usuario = new Usuario("Juan", "Pérez", "juan.perez@example.com", password, true);
            string incorrectPassword = "WrongPass456!";

            // Act - Intenta verificar una contraseña incorrecta
            bool resultado = usuario.CompruebaPassword(incorrectPassword);

            // Assert - Verifica que la contraseña es incorrecta y que la fecha de último inicio no se ha actualizado
            Assert.IsFalse(resultado);
            Assert.AreEqual(DateTime.MinValue, usuario.UltimoInicioSesion); // Verifica que la fecha de último inicio no se ha actualizado
        }
    }
}
