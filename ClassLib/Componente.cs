using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    // Clase componente, en este caso artículos que pertenencen a usuarios de nuestra web multiusuario
    public class Componente
    {
        private static int contadorId = 0; // Variable estática para el contador de ID, es global
        private int _idElemento; // ID del componente
        private int _identificadorUsuario; // ID del usuario que crea el articulo
        private string _nomElemento; // Nombre del artículo
        private string _desElemento; // Descripción del artículo

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Componente"/> con los datos proporcionados.
        /// </summary>
        /// <param name="identificadorUsuario">El ID del usuario autor del componente.</param>
        /// <param name="nomElemento">El título o nombre del componente.</param>
        /// <param name="desElemento">La descripción o contenido del componente.</param>
        /// <remarks>
        /// Este constructor permite crear un nuevo componente asociado a un usuario específico, 
        /// estableciendo el título y el contenido del artículo correspondiente.
        /// </remarks>
        public Componente(int identificadorUsuario, string nomElemento, string desElemento)
        {
            if (string.IsNullOrWhiteSpace(nomElemento))
            {
                throw new ArgumentNullException(nameof(nomElemento));
            }

            // Asignamos el próximo ID único y luego incrementa el contador
            _idElemento = ++contadorId;
            // Asignamos los argumentos pasados al constructor
            _identificadorUsuario = identificadorUsuario;
            _nomElemento = nomElemento;
            _desElemento = desElemento;
        }

        // Propiedades del artículo
        public string NomElemento { get => _nomElemento; set => _nomElemento = value; }
        public string DesElemento { get => _desElemento; set => _desElemento = value; }
        public int IdElemento { get => _idElemento; }
        public int IdentificadorUsuario { get => _identificadorUsuario; set => _identificadorUsuario = value; }

        public class Division : Componente
        {
            private List<Componente> contenido;

            public List<Componente> Contenido { get => contenido; set => contenido = value; }

            // Llamada al constructor base
            public Division(int identificadorUsuario, string nomElemento, string desElemento) : base(identificadorUsuario, nomElemento, desElemento)
            {
                contenido = new List<Componente>();
            }

            public int NumElementos()
            {
                return contenido.Count;
            }
        }
    }
}
