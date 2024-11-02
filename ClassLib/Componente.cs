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
        private int _idElemento = -1;
        private string _nomElemento = "";
        private string _desElemento = "";

        public Componente(string nomElemento, string desElemento) {

            if (string.IsNullOrWhiteSpace(nomElemento)) {
                throw new ArgumentNullException();
            }
          
            IdElemento = -1;
            _nomElemento = nomElemento;
            _desElemento = desElemento;
        }

        public string NomElemento { get => _nomElemento; set => _nomElemento = value; }
        protected string DesElemento { get => _desElemento; set => _desElemento = value; }
        protected int IdElemento { get => _idElemento; set => _idElemento = value; }


        public class Division : Componente {

            private List<Componente> contenido;

            public List<Componente> Contenido { get => contenido; set => contenido = value; }

            public Division(String _nomElemento, string _desElemento)
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
