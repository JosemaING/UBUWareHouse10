using Microsoft.VisualStudio.TestTools.UnitTesting;
using UBULib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBULib.Tests
{
    [TestClass()]
    public class UtilidadesTests
    {
        [TestMethod()]
        public void EncriptarTest()
        {

            string password0 = "Pa$$w0rd";
            string password1 = "Pa$$w0rd";
            string password2 = "";
            string password3 = "P@$$w0rd";

            Utilidades utilidades = new Utilidades();

            Assert.AreEqual(utilidades.Encriptar(password0), utilidades.Encriptar(password1));
            Assert.AreNotEqual(utilidades.Encriptar(password0), utilidades.Encriptar(password2));
            Assert.AreNotEqual(utilidades.Encriptar(password0), utilidades.Encriptar(password3));
        }

        [DataTestMethod]
        [DataRow("abc123", 4)]
        [DataRow("ABCDEFG", 3)]
        [DataRow("!@#$$", 3)]
        [DataRow("abcdef", 3)]
        [DataRow("123456", 3)]
        [DataRow("Aa1!2345", 6)]
        [DataRow("abcdefghijklmnop", 3)]
        [DataRow("A1!", 5)]
        [DataRow("", 0)]
        [DataRow("LongPassword123!", 6)]
        public void CompruebaPasswordTest(string cadena, int esperado)
        {
            Utilidades utilidades = new Utilidades();

            Assert.IsTrue(utilidades.CompruebaPassword(cadena) == esperado, cadena);
        }

        [DataTestMethod]
        [DataRow("abc123", "Débil")]
        [DataRow("ABCDEFG", "Débil")]
        [DataRow("!@#$$", "Débil")]
        [DataRow("Aa1", "Moderada")]
        [DataRow("Aa1!", "Fuerte")]
        public void VerificaFortalezaPasswordTest(string password, string esperado)
        {
            Utilidades utilidades = new Utilidades();

            Assert.AreEqual(esperado, utilidades.VerificaFortalezaPassword(password), $"Contraseña: {password}");
        }


        [TestMethod]
        public void GenerarContrasenaSeguraTest()
        {
            Utilidades utilidades = new Utilidades();

            // Genera una contraseña de longitud específica
            string contrasena = utilidades.GenerarContrasenaSegura(16);
            Assert.AreEqual(16, contrasena.Length);

            // Comprueba que contenga solo caracteres válidos
            const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789[]{}()!¡¿?=.,;:|@#$€+*/_-";
            Assert.IsTrue(contrasena.All(c => caracteresPermitidos.Contains(c)), "La contraseña contiene caracteres no permitidos.");
        }

    }

}