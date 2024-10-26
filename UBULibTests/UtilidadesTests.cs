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
    }
}