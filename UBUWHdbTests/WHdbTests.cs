using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLib;
using UBUWHdb;
using System.Collections.Generic;

namespace UBUWHdb.Tests
{
    [TestClass]
    public class WHdbTests
    {
        private WHdb db;

        [TestInitialize]
        public void TestInitialize()
        {
            db = new WHdb();
        }

        [TestMethod]
        public void GuardaUsuario_NuevoUsuario_DevuelveTrue()
        {
            // Arrange - Crea un nuevo usuario
            Usuario usuario = new Usuario("Ana", "García", "ana@example.com", "password", false);

            // Act - Intenta guardar el usuario en la base de datos
            bool resultado = db.GuardaUsuario(usuario);

            // Assert - Verifica que el usuario fue guardado exitosamente y se puede leer
            Assert.IsTrue(resultado);
            Assert.IsNotNull(db.LeeUsuario("ana@example.com"));
        }

        [TestMethod]
        public void LeeUsuario_EmailExistente_DevuelveUsuario()
        {
            // Arrange - Define un email de un usuario que existe en la base de datos
            string email = "iml1012@alu.ubu.es";

            // Act - Intenta leer el usuario por su email
            Usuario usuario = db.LeeUsuario(email);

            // Assert - Verifica que el usuario se obtuvo correctamente
            Assert.IsNotNull(usuario);
            Assert.AreEqual(email, usuario.Email);
        }

        [TestMethod]
        public void LeeUsuario_EmailNoExistente_DevuelveNull()
        {
            // Act - Intenta leer un usuario que no existe en la base de datos
            Usuario usuario = db.LeeUsuario("noexiste@example.com");

            // Assert - Verifica que el método devuelve null para un email inexistente
            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void ValidaUsuario_ContrasenaCorrecta_DevuelveTrue()
        {
            // Arrange - Define un email y contraseña correctos de un usuario existente
            string email = "iml1012@alu.ubu.es";
            string password = "Estudiante1";

            // Act - Intenta validar el usuario con la contraseña correcta
            bool resultado = db.ValidaUsuario(email, password);

            // Assert - Verifica que la validación fue exitosa
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ValidaUsuario_ContrasenaIncorrecta_DevuelveFalse()
        {
            // Arrange - Define un email correcto y una contraseña incorrecta
            string email = "iml1012@alu.ubu.es";
            string password = "ContraseñaIncorrecta";

            // Act - Intenta validar el usuario con una contraseña incorrecta
            bool resultado = db.ValidaUsuario(email, password);

            // Assert - Verifica que la validación falló
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void GuardaComponente_NuevoComponente_DevuelveTrue()
        {
            // Arrange - Crea un nuevo componente
            Componente componente = new Componente(0, "Componente 1", "Descripción del componente");

            // Act - Intenta guardar el componente en la base de datos
            bool resultado = db.GuardaComponente(componente);

            // Assert - Verifica que el componente fue guardado y el número de componentes es correcto
            Assert.IsTrue(resultado);
            Assert.AreEqual(1, db.ObtenerNumeroDeComponentes(0));
        }

        [TestMethod]
        public void LeeComponente_ComponenteExistente_DevuelveComponente()
        {
            // Arrange - Crea y guarda un componente en la base de datos
            Componente componente = new Componente(0, "Componente 1", "Descripción del componente");
            db.GuardaComponente(componente);

            // Act - Intenta leer el componente por su ID
            Componente obtenido = db.LeeComponente(componente.IdElemento);

            // Assert - Verifica que el componente se obtuvo correctamente
            Assert.IsNotNull(obtenido);
            Assert.AreEqual(componente.IdElemento, obtenido.IdElemento);
        }

        [TestMethod]
        public void CambiaPassword_UsuarioExistente_DevuelveTrue()
        {
            // Arrange - Define el ID de un usuario existente y una nueva contraseña
            int idUsuario = 0;
            string nuevaPassword = "NuevaPassword123";

            // Act - Intenta cambiar la contraseña del usuario
            bool resultado = db.CambiaPassword(idUsuario, nuevaPassword);

            // Assert - Verifica que el cambio fue exitoso y la nueva contraseña se ha guardado
            Assert.IsTrue(resultado);
            Assert.AreEqual(nuevaPassword, db.LeeUsuarioPorId(idUsuario).Password);
        }

        [TestMethod]
        public void DesactivaUsuario_UsuarioExistente_DevuelveTrue()
        {
            // Arrange - Define el ID de un usuario existente
            int idUsuario = 1;

            // Act - Intenta desactivar al usuario
            bool resultado = db.DesactivaUsuario(idUsuario);

            // Assert - Verifica que la desactivación fue exitosa y el usuario está inactivo
            Assert.IsTrue(resultado);
            Assert.IsFalse(db.LeeUsuarioPorId(idUsuario).Activo);
        }

        [TestMethod]
        public void EliminarUsuario_UsuarioExistente_DevuelveTrue()
        {
            // Arrange - Define el ID de un usuario existente y asegura que está en la base de datos
            int idUsuario = 1;
            WHdb db = new WHdb();

            // Asegurarse de que el usuario existe antes de intentar eliminarlo
            Assert.IsNotNull(db.LeeUsuarioPorId(idUsuario));

            // Act - Intenta eliminar al usuario
            bool resultado = db.EliminarUsuario(idUsuario);

            // Assert - Verifica que el usuario fue eliminado correctamente
            Assert.IsTrue(resultado); // Verifica que el método devolvió true
            Assert.IsNull(db.LeeUsuarioPorId(idUsuario)); // Verifica que el usuario ya no existe
        }

        [TestMethod]
        public void EliminarUsuario_UsuarioInexistente_DevuelveFalse()
        {
            // Arrange - Define un ID de usuario inexistente
            int idUsuario = 999; // ID de usuario que no existe
            WHdb db = new WHdb();

            // Act - Intenta eliminar un usuario que no existe
            bool resultado = db.EliminarUsuario(idUsuario);

            // Assert - Verifica que el método devuelve false
            Assert.IsFalse(resultado); // Verifica que el método devolvió false
        }
    }
}
