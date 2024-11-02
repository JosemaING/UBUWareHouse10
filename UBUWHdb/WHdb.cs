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
        // Inicialmente es 3 porque los primeros usuarios ya están registrados con IDs 0, 1 y 2.
        int siguienteUsuarioId = 3;

        // Diccionario que almacena los componentes asociados a cada usuario.
        // La clave (int) es el ID del usuario, y el valor es una lista de objetos Componente pertenecientes a ese usuario.
        private Dictionary<int, List<Componente>> componentesPorUsuario = new Dictionary<int, List<Componente>>();

        public WHdb() {

            tblUsuarios[0] = new Usuario("Ismael", "Manzanera López", "iml1012@alu.ubu.es", "Estudiante1", false);
            tblUsuarios[1] = new Usuario("Jose Maria", "Santos Romero", "jsr1002@alu.ubu.es", "Estudiante2", false);
            tblUsuarios[2] = new Usuario("Pedro", "Renedo Fernandez", "prenedo@ubu.es", "Profesor1", false);

            tblUsuarios[0].EsGestor = false;
            tblUsuarios[1].EsGestor = true;

            tblUsuarios[0].Activo = false;

            tblUsuarios[0].IdUsuario = 0;
            tblUsuarios[1].IdUsuario = 1;
            tblUsuarios[2].IdUsuario = 2;
        }

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

        public Usuario LeeUsuario(string email)
        {
            // Iteramos sobre los valores de tblUsuarios para encontrar el usuario con el email especificado
            return tblUsuarios.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

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

        public int NumUsuarios()
        {
            return tblUsuarios.Count;
        }

        public int NumUsuariosActivos()
        {
            return tblUsuarios.Values.Count(u => u.Activo);
        }

        public bool GuardaComponente(Componente e)
        {
            if (!componentesPorUsuario.ContainsKey(e.IdentificadorUsuario))
            {
                componentesPorUsuario[e.IdentificadorUsuario] = new List<Componente>();
            }
            componentesPorUsuario[e.IdentificadorUsuario].Add(e);
            return true;
        }

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

        public int NumComponente()
        {
            return componentesPorUsuario.Values.Sum(lista => lista.Count);
        }

        public List<Componente> ObtenerComponentesPorUsuario(int idUsuario)
        {
            if (componentesPorUsuario.ContainsKey(idUsuario))
            {
                return componentesPorUsuario[idUsuario];
            }
            return new List<Componente>();
        }

        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            // Devuelve una lista de todos los valores en el diccionario de usuarios
            return tblUsuarios.Values.ToList();
        }

        public bool DesactivaUsuario(int idUsuario)
        {
            if (tblUsuarios.ContainsKey(idUsuario))
            {
                tblUsuarios[idUsuario].Activo = false;
                return true;
            }
            return false;
        }

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

        public int ObtenerNumeroDeComponentes(int idUsuario)
        {
            // Llama al método ObtenerComponentesPorUsuario para obtener la lista de componentes del usuario
            List<Componente> componentes = ObtenerComponentesPorUsuario(idUsuario);

            // Devuelve la cantidad de componentes en la lista
            return componentes.Count;
        }

        public Usuario LeeUsuarioPorId(int idUsuario)
        {
            // Comprueba si el usuario existe en el diccionario y lo devuelve; de lo contrario, devuelve null
            if (tblUsuarios.ContainsKey(idUsuario))
            {
                return tblUsuarios[idUsuario];
            }
            return null;
        }
    }
}
