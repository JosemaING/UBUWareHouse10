using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class Componente
    {
        // Variable estática para el contador de ID
        private static int contadorId = 0;

        private int _idElemento; // ID de la nota
        private int _identificadorUsuario; // ID del usuario que crea la nota
        private string _nomElemento; // Nombre de la nota
        private string _desElemento; // Contenido de la nota

        public Componente(int identificadorUsuario, string nomElemento, string desElemento)
        {
            if (string.IsNullOrWhiteSpace(nomElemento))
            {
                throw new ArgumentNullException(nameof(nomElemento));
            }

            // Asignar el próximo ID único y luego incrementar el contador
            _idElemento = ++contadorId;
            _identificadorUsuario = identificadorUsuario;
            _nomElemento = nomElemento;
            _desElemento = desElemento;
        }

        // Propiedades
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
