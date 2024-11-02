using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLib;
using System;
using System.Collections.Generic;

namespace ClassLib.Tests
{
    [TestClass]
    public class ComponenteTests
    {
        [TestMethod]
        public void Constructor_ComponenteAsignacionCorrecta()
        {
            // Arrange
            int identificadorUsuario = 1;
            string nomElemento = "Test Componente";
            string desElemento = "Descripción de prueba";

            // Act
            Componente componente = new Componente(identificadorUsuario, nomElemento, desElemento);

            // Assert
            Assert.AreEqual(identificadorUsuario, componente.IdentificadorUsuario);
            Assert.AreEqual(nomElemento, componente.NomElemento);
            Assert.AreEqual(desElemento, componente.DesElemento);
            Assert.IsTrue(componente.IdElemento > 0); // Verifica que el ID se ha asignado
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NomElementoNull_LanzaExcepcion()
        {
            // Arrange
            int identificadorUsuario = 1;
            string nomElemento = null;
            string desElemento = "Descripción de prueba";

            // Act
            Componente componente = new Componente(identificadorUsuario, nomElemento, desElemento);
        }

        [TestMethod]
        public void Propiedades_ComponenteActualizarValores()
        {
            // Arrange
            Componente componente = new Componente(1, "Nombre Original", "Descripción Original");

            // Act
            componente.NomElemento = "Nombre Actualizado";
            componente.DesElemento = "Descripción Actualizada";

            // Assert
            Assert.AreEqual("Nombre Actualizado", componente.NomElemento);
            Assert.AreEqual("Descripción Actualizada", componente.DesElemento);
        }

        [TestMethod]
        public void Division_NumElementos_SinElementosDevuelveCero()
        {
            // Arrange
            Componente.Division division = new Componente.Division(1, "División Test", "Descripción División");

            // Act
            int numElementos = division.NumElementos();

            // Assert
            Assert.AreEqual(0, numElementos);
        }

        [TestMethod]
        public void Division_NumElementos_ConElementosDevuelveCorrecto()
        {
            // Arrange
            Componente.Division division = new Componente.Division(1, "División Test", "Descripción División");
            Componente componente1 = new Componente(1, "Componente 1", "Descripción 1");
            Componente componente2 = new Componente(2, "Componente 2", "Descripción 2");

            // Act
            division.Contenido.Add(componente1);
            division.Contenido.Add(componente2);
            int numElementos = division.NumElementos();

            // Assert
            Assert.AreEqual(2, numElementos);
        }
    }
}
