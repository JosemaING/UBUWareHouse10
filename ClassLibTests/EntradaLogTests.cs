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
            // Arrange - Define los valores de usuario y evento para inicializar el log de entrada
            string usuario = "usuario@example.com";
            string evento = "Inicio de sesión";

            // Act - Crea una instancia de EntradaLog con los valores iniciales
            EntradaLog entrada = new EntradaLog(usuario, evento);

            // Assert - Verifica que las propiedades se asignaron correctamente
            Assert.AreEqual(usuario, entrada.Usuario);
            Assert.AreEqual(evento, entrada.Evento);
            Assert.IsTrue((DateTime.Now - entrada.FechaEvento).TotalSeconds < 1); // Verifica que la fecha es cercana a la actual
        }

        [TestMethod]
        public void Propiedades_ActualizarValores()
        {
            // Arrange - Crea una entrada log con valores iniciales
            EntradaLog entrada = new EntradaLog("usuario@example.com", "Evento inicial");

            // Act - Actualiza las propiedades de usuario y evento en la entrada log
            entrada.Usuario = "nuevoUsuario@example.com";
            entrada.Evento = "Evento actualizado";

            // Assert - Verifica que los valores se han actualizado correctamente
            Assert.AreEqual("nuevoUsuario@example.com", entrada.Usuario);
            Assert.AreEqual("Evento actualizado", entrada.Evento);
        }
    }
}
