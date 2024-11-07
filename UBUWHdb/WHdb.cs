using ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UBUWHdb
{
    public class WHdb : IWHdb
    {
        // Diccionario que almacena los usuarios registrados en el sistema.
        // La clave (int) es el ID único de cada usuario, y el valor es el objeto Usuario.
        Dictionary<int, Usuario> tblUsuarios = new Dictionary<int, Usuario>();

        // ID que se asignará al siguiente usuario que se registre.
        // Inicialmente es 4 porque los primeros usuarios ya están registrados con IDs 0, 1, 2 y 3.
        int siguienteUsuarioId = 4;

        // Diccionario que almacena los componentes asociados a cada usuario.
        // La clave (int) es el ID del usuario, y el valor es una lista de objetos Componente pertenecientes a ese usuario.
        private Dictionary<int, List<Componente>> componentesPorUsuario = new Dictionary<int, List<Componente>>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="WHdb"/>, es la base de datos.
        /// </summary>
        /// <remarks>
        /// Este constructor crea una base de datos, además la base de datos tiene una serie de usuarios
        /// que han sido utilizados para realizar tests unitarios y pruebas a la web.
        /// </remarks>
        public WHdb() {
            // Se crean nuevos usuarios
            tblUsuarios[0] = new Usuario("Ismael", "Manzanera López", "iml1012@alu.ubu.es", "Estudiante1", false);
            tblUsuarios[1] = new Usuario("Jose Maria", "Santos Romero", "jsr1002@alu.ubu.es", "Estudiante2", false);
            tblUsuarios[2] = new Usuario("Pedro", "Renedo Fernandez", "prenedo@ubu.es", "Profesor1", true);
            tblUsuarios[3] = new Usuario("Ana", "Gimenez Bernal", "agb1111@alu.ubu.es", "Estudiante3", false);
            // Se asignan manualmente los parametros que nos interesan para realizar pruebas
            tblUsuarios[0].EsGestor = false; // Ismael
            tblUsuarios[1].EsGestor = true; // Jose Maria
            tblUsuarios[3].EsGestor = true; // Ana

            tblUsuarios[1].CaducidadPassword = DateTime.Now; // Jose Maria

            tblUsuarios[0].Activo = false; // Ismael

            tblUsuarios[0].IdUsuario = 0; // Ismael
            tblUsuarios[1].IdUsuario = 1; // Jose Maria
            tblUsuarios[2].IdUsuario = 2; // Pedro
            tblUsuarios[3].IdUsuario = 3; // Ana
        }

        // Funcion que guarda un nuevo usuario en la base de datos
        public bool GuardaUsuario(Usuario u)
        {
            // Verifica si el usuario ya tiene un Id, de lo contrario asigna uno nuevo
            if (u.IdUsuario < 0)
            {
                u.IdUsuario = siguienteUsuarioId;
                siguienteUsuarioId++;
            }

            // Guarda el usuario en el diccionario
            tblUsuarios[u.IdUsuario] = u;
            return true;
        }

        // Funcion que devuelve un objeto usuario según su email
        public Usuario LeeUsuario(string email)
        {
            // Iteramos sobre los valores de tblUsuarios para encontrar el usuario con el email especificado
            return tblUsuarios.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // Funcion que comprueba si el usuario puede iniciar sesion a la web
        public bool ValidaUsuario(string email, string password)
        {
            Usuario usuario = LeeUsuario(email);
            if (usuario != null && usuario.CompruebaPassword(password))
            {
                usuario.Activo = true;
                return true;
            }
            return false;
        }

        // Funcion que devuelve el numero de usuarios
        public int NumUsuarios()
        {
            return tblUsuarios.Count;
        }

        // Funcion que devuelve el numero de usuarios activos
        public int NumUsuariosActivos()
        {
            return tblUsuarios.Values.Count(u => u.Activo);
        }

        // Funcion que almacena un nuevo componente en la base de datos
        public bool GuardaComponente(Componente e)
        {
            if (!componentesPorUsuario.ContainsKey(e.IdentificadorUsuario))
            {
                componentesPorUsuario[e.IdentificadorUsuario] = new List<Componente>();
            }
            componentesPorUsuario[e.IdentificadorUsuario].Add(e);
            return true;
        }

        // Funcion que devuelve el componente según su identificador unico
        public Componente LeeComponente(int idElemento)
        {
            // Buscar en todos los componentes de todos los usuarios
            foreach (var listaComponentes in componentesPorUsuario.Values)
            {
                var componente = listaComponentes.FirstOrDefault(c => c.IdElemento == idElemento);
                if (componente != null)
                {
                    return componente;
                }
            }
            return null; // Retorna null si no se encuentra el componente
        }

        // Funcion que devuelve el numero de componentes que hay en la web
        public int NumComponente()
        {
            return componentesPorUsuario.Values.Sum(lista => lista.Count);
        }

        // Funcion que devuelve el nuemro de componentes que pertenecen a cadda usuario
        public List<Componente> ObtenerComponentesPorUsuario(int idUsuario)
        {
            if (componentesPorUsuario.ContainsKey(idUsuario))
            {
                return componentesPorUsuario[idUsuario];
            }
            return new List<Componente>();
        }

        // Funcion que devuelvbe una lista de todos los usuarios registrados
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            // Devuelve una lista de todos los valores en el diccionario de usuarios
            return tblUsuarios.Values.ToList();
        }

        // Funcion que desactivca el acceso a la web para un usuario concreto
        public bool DesactivaUsuario(int idUsuario)
        {
            if (tblUsuarios.ContainsKey(idUsuario))
            {
                tblUsuarios[idUsuario].Activo = false;
                return true;
            }
            return false;
        }

        // Funcion que cambia la contraseña de in usuario
        public bool CambiaPassword(int idUsuario, string nuevaPassword)
        {
            if (tblUsuarios.ContainsKey(idUsuario))
            {
                Usuario usuario = tblUsuarios[idUsuario];
                if (usuario.PasswordAnteriores.Count >= Usuario.CANTIDAD_PASSWORD_RECORDADAS)
                {
                    usuario.PasswordAnteriores.RemoveAt(0);
                }
                usuario.PasswordAnteriores.Add(usuario.Password);
                usuario.Password = nuevaPassword;
                usuario.CaducidadPassword = DateTime.Now.AddMonths(6);
                return true;
            }
            return false;
        }

        // Funcion que obtiene el numero de componentes que pertenecen a un usuario concreto
        public int ObtenerNumeroDeComponentes(int idUsuario)
        {
            // Llama al método ObtenerComponentesPorUsuario para obtener la lista de componentes del usuario
            List<Componente> componentes = ObtenerComponentesPorUsuario(idUsuario);

            // Devuelve la cantidad de componentes en la lista
            return componentes.Count;
        }
        // Funcion que devuelve el objeto usuario segun su id
        public Usuario LeeUsuarioPorId(int idUsuario)
        {
            // Comprueba si el usuario existe en el diccionario y lo devuelve; de lo contrario, devuelve null
            if (tblUsuarios.ContainsKey(idUsuario))
            {
                return tblUsuarios[idUsuario];
            }
            return null;
        }

        // Funcion que elimina un usuario de la base de datos
        public bool EliminarUsuario(int idUsuario)
        {
            // Comprueba si el usuario existe en el diccionario
            if (tblUsuarios.ContainsKey(idUsuario))
            {
                // Remueve el usuario del diccionario
                tblUsuarios.Remove(idUsuario);
                return true; // Indica que la eliminación fue exitosa
            }
            return false; // Indica que el usuario no fue encontrado
        }

    }
}
