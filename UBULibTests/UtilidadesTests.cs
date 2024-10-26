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
            Utilidades utilidades = new Utilidades();
            Assert.AreEqual(5, utilidades.CompruebaPassword("Simple123")); // letras minúsculas, números, longitud menor a 16, letras mayúsculas
            Assert.AreEqual(3, utilidades.CompruebaPassword("123456")); // números, longitud menor a 16
            Assert.AreEqual(6, utilidades.CompruebaPassword("Compl3xP@ss!")); // cumple con todos los criterios
        }

        [TestMethod()]
        public void VerificaFortalezaPasswordTest()
        {
            Utilidades utilidades = new Utilidades();
            Assert.AreEqual("Débil", utilidades.VerificaFortalezaPassword("1234"));
            Assert.AreEqual("Moderada", utilidades.VerificaFortalezaPassword("Simple123"));
            Assert.AreEqual("Fuerte", utilidades.VerificaFortalezaPassword("Compl3xP@ss!"));
        }

        [TestMethod()]
        public void IntentoFallidoTest()
        {
            Utilidades utilidades = new Utilidades();

            // Realizar 4 intentos fallidos, la cuenta no debe bloquearse
            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(utilidades.IntentoFallido());
            }

            // En el quinto intento, la cuenta se debe bloquear
            Assert.IsFalse(utilidades.IntentoFallido());

            // Intento después del bloqueo, aún debe estar bloqueado
            Assert.IsFalse(utilidades.IntentoFallido());
        }

        [TestMethod()]
        public void ResetearIntentosTest()
        {
            Utilidades utilidades = new Utilidades();

            // Realizar varios intentos fallidos
            for (int i = 0; i < 5; i++)
            {
                utilidades.IntentoFallido();
            }

            // Resetear intentos fallidos
            utilidades.ResetearIntentos();

            // Asegurarse de que se permita otro intento después de resetear
            Assert.IsTrue(utilidades.IntentoFallido());
        }

        [TestMethod()]
        public void GenerarContrasenaSeguraTest()
        {
            Utilidades utilidades = new Utilidades();

            string contrasena1 = utilidades.GenerarContrasenaSegura();
            string contrasena2 = utilidades.GenerarContrasenaSegura();

            // Verificar longitud por defecto
            Assert.AreEqual(16, contrasena1.Length);

            // Verificar que se generen contraseñas distintas en diferentes llamadas
            Assert.AreNotEqual(contrasena1, contrasena2);

            // Verificar longitud personalizada
            string contrasenaPersonalizada = utilidades.GenerarContrasenaSegura(20);
            Assert.AreEqual(20, contrasenaPersonalizada.Length);
        }

        [TestMethod()]
        public void EsContrasenaReutilizadaTest()
        {
            Utilidades utilidades = new Utilidades();

            // Guardar una contraseña y verificar si es reutilizada
            utilidades.GuardarContrasena("password123");
            Assert.IsTrue(utilidades.EsContrasenaReutilizada("password123"));
            Assert.IsFalse(utilidades.EsContrasenaReutilizada("nuevaPassword"));
        }

        [TestMethod()]
        public void GuardarContrasenaTest()
        {
            Utilidades utilidades = new Utilidades();

            // Añadir contraseñas al historial
            utilidades.GuardarContrasena("password1");
            utilidades.GuardarContrasena("password2");
            utilidades.GuardarContrasena("password3");
            utilidades.GuardarContrasena("password4");
            utilidades.GuardarContrasena("password5");

            // Verificar que no se ha eliminado ninguna de las primeras
            Assert.IsTrue(utilidades.EsContrasenaReutilizada("password1"));

            // Añadir una nueva contraseña para exceder el límite del historial
            utilidades.GuardarContrasena("password6");

            // Verificar que la primera contraseña se eliminó
            Assert.IsFalse(utilidades.EsContrasenaReutilizada("password1"));
            Assert.IsTrue(utilidades.EsContrasenaReutilizada("password6"));
        }
    }
}