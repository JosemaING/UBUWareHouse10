using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class EntradaLog
    {
        private DateTime _fechaEvento;
        private string _usuario;
        private string _evento;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EntradaLog"/> con los datos proporcionados.
        /// </summary>
        /// <param name="usuario">Email del usuario que crea el evento.</param>
        /// <param name="evento">Nombre del evento creado.</param>
        /// <remarks>
        /// Este constructor permite crea una nueva entrada a nuestro log, 
        /// estableciendo el email del usuario que realiza el evento y el evento es sí.
        /// </remarks>
        public EntradaLog (string usuario, string evento)
        {
            // La fecha del evento es en el momento en que se crea la entrada
            _fechaEvento = DateTime.Now;
            // Asignamos los parametros pasados como argumentos
            Usuario = usuario;
            Evento = evento;
        }
        // Métodos getter y setter para la clase EntradaLog
        public DateTime FechaEvento { get { return _fechaEvento;}}
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Evento { get => _evento; set => _evento = value; }
        
    }
}
