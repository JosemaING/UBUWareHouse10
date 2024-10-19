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

        [TestMethod()]
        public void CompruebaPasswordTest()
        {
            Assert.Fail();
        }
    }
}