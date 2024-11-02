using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLib;
using System;

namespace ClassLib.Tests
{
    [TestClass]
    public class EntradaLogTests
    {
        [TestMethod]
        public void Constructor_AsignacionCorrecta()
        {
            // Arrange
            string usuario = "usuario@example.com";
            string evento = "Inicio de sesión";

            // Act
            EntradaLog entrada = new EntradaLog(usuario, evento);

            // Assert
            Assert.AreEqual(usuario, entrada.Usuario);
            Assert.AreEqual(evento, entrada.Evento);
            Assert.IsTrue((DateTime.Now - entrada.FechaEvento).TotalSeconds < 1); // Verifica que la fecha es cercana a la actual
        }

        [TestMethod]
        public void Propiedades_ActualizarValores()
        {
            // Arrange
            EntradaLog entrada = new EntradaLog("usuario@example.com", "Evento inicial");

            // Act
            entrada.Usuario = "nuevoUsuario@example.com";
            entrada.Evento = "Evento actualizado";

            // Assert
            Assert.AreEqual("nuevoUsuario@example.com", entrada.Usuario);
            Assert.AreEqual("Evento actualizado", entrada.Evento);
        }
    }
}
