using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    internal class EntradaLog
    {
        private DateTime _fechaEvento;
        private string _usuario;
        private string _evento;
    
        public EntradaLog (string usuario, string evento)
        {
            _fechaEvento = DateTime.Now;
            Usuario = usuario;
            Evento = evento;
        }

        public DateTime FechaEvento { get { return _fechaEvento;}}
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Evento { get => _evento; set => _evento = value; }
        
    }
}
