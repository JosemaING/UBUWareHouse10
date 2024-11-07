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
            // Arrange - Configura los datos de entrada para el constructor
            int identificadorUsuario = 1;
            string nomElemento = "Test Componente";
            string desElemento = "Descripción de prueba";

            // Act - Crea una instancia de Componente con los datos de entrada
            Componente componente = new Componente(identificadorUsuario, nomElemento, desElemento);

            // Assert - Verifica que las propiedades se asignaron correctamente
            Assert.AreEqual(identificadorUsuario, componente.IdentificadorUsuario);
            Assert.AreEqual(nomElemento, componente.NomElemento);
            Assert.AreEqual(desElemento, componente.DesElemento);
            Assert.IsTrue(componente.IdElemento > 0); // Verifica que el ID se ha asignado
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NomElementoNull_LanzaExcepcion()
        {
            // Arrange - Configura datos con un nombre nulo para el elemento
            int identificadorUsuario = 1;
            string nomElemento = null;
            string desElemento = "Descripción de prueba";

            // Act - Intenta crear Componente con un nombre nulo, lo que debería lanzar una excepción
            Componente componente = new Componente(identificadorUsuario, nomElemento, desElemento);
        }

        [TestMethod]
        public void Propiedades_ComponenteActualizarValores()
        {
            // Arrange - Crea una instancia de Componente con valores iniciales
            Componente componente = new Componente(1, "Nombre Original", "Descripción Original");

            // Act - Actualiza las propiedades del componente
            componente.NomElemento = "Nombre Actualizado";
            componente.DesElemento = "Descripción Actualizada";

            // Assert - Verifica que las propiedades se hayan actualizado correctamente
            Assert.AreEqual("Nombre Actualizado", componente.NomElemento);
            Assert.AreEqual("Descripción Actualizada", componente.DesElemento);
        }

        [TestMethod]
        public void Division_NumElementos_SinElementosDevuelveCero()
        {
            // Arrange - Crea una instancia de División sin elementos
            Componente.Division division = new Componente.Division(1, "División Test", "Descripción División");

            // Act - Obtiene el número de elementos en la división
            int numElementos = division.NumElementos();

            // Assert - Verifica que la cuenta de elementos es cero
            Assert.AreEqual(0, numElementos);
        }

        [TestMethod]
        public void Division_NumElementos_ConElementosDevuelveCorrecto()
        {
            // Arrange - Crea una instancia de División y añade dos componentes
            Componente.Division division = new Componente.Division(1, "División Test", "Descripción División");
            Componente componente1 = new Componente(1, "Componente 1", "Descripción 1");
            Componente componente2 = new Componente(2, "Componente 2", "Descripción 2");

            // Act - Agrega componentes a la división y cuenta el número de elementos
            division.Contenido.Add(componente1);
            division.Contenido.Add(componente2);
            int numElementos = division.NumElementos();

            // Assert - Verifica que la cuenta de elementos es correcta (2)
            Assert.AreEqual(2, numElementos);
        }
    }
}
