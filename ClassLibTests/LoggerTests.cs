using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLib.Tests
{
    [TestClass]
    public class LoggerTests
    {
        private string rutaTemporal;

        [TestInitialize]
        public void TestInitialize()
        {
            // Crear una ruta temporal para el archivo de log
            rutaTemporal = Path.Combine(Path.GetTempPath(), "test_log.txt");

            // Asegurarse de que el archivo no exista antes de cada prueba
            if (File.Exists(rutaTemporal))
            {
                File.Delete(rutaTemporal);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Eliminar el archivo de log después de cada prueba
            if (File.Exists(rutaTemporal))
            {
                File.Delete(rutaTemporal);
            }
        }

        [TestMethod]
        public void RegistrarEvento_AgregaLogALaLista()
        {
            // Arrange - Crea una instancia de Logger y define el usuario y evento a registrar
            Logger logger = new Logger(rutaTemporal);
            string usuario = "usuario@example.com";
            string evento = "Inicio de sesión";

            // Act - Registra el evento en el logger y obtiene la lista de logs
            logger.RegistrarEvento(usuario, evento);
            List<EntradaLog> logs = logger.ObtenerLogs();

            // Assert - Verifica que se agregó un log a la lista con los valores correctos
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(usuario, logs[0].Usuario);
            Assert.AreEqual(evento, logs[0].Evento);
        }

        [TestMethod]
        public void RegistrarEvento_GuardaEnArchivo()
        {
            // Arrange - Crea una instancia de Logger y define usuario y evento a registrar
            Logger logger = new Logger(rutaTemporal);
            string usuario = "usuario@example.com";
            string evento = "Inicio de sesión";

            // Act - Registra el evento en el logger
            logger.RegistrarEvento(usuario, evento);

            // Assert - Verifica que se haya creado el archivo de log
            Assert.IsTrue(File.Exists(rutaTemporal), "El archivo de log debería haberse creado.");
            // Leer el contenido del archivo y verificar que contiene el evento registrado
            string contenidoArchivo = File.ReadAllText(rutaTemporal);
            Assert.IsTrue(contenidoArchivo.Contains(usuario));
            Assert.IsTrue(contenidoArchivo.Contains(evento));
        }

        [TestMethod]
        public void ObtenerLogs_DevuelveListaCorrecta()
        {
            // Arrange - Crea una instancia de Logger y registra dos eventos diferentes
            Logger logger = new Logger(rutaTemporal);
            logger.RegistrarEvento("usuario1@example.com", "Evento 1");
            logger.RegistrarEvento("usuario2@example.com", "Evento 2");

            // Act - Obtiene la lista de logs registrados
            List<EntradaLog> logs = logger.ObtenerLogs();

            // Assert - Verifica que la lista contiene ambos eventos con los valores correctos
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual("usuario1@example.com", logs[0].Usuario);
            Assert.AreEqual("Evento 1", logs[0].Evento);
            Assert.AreEqual("usuario2@example.com", logs[1].Usuario);
            Assert.AreEqual("Evento 2", logs[1].Evento);
        }
    }
}
