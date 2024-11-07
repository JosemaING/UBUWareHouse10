using System.Collections.Generic;
using System;
using System.IO;

namespace ClassLib
{
    public class Logger
    {
        // Lista de entradas que tendrá los eventos ocurridos
        private List<EntradaLog> _logs = new List<EntradaLog>();
        // Ruta de nuestro fichero log.txt
        private string _rutaLog;

        /// <summary>
        /// Inicializa una nueva instancia del logger <see cref="Logger"/> con la ruta del fichero log.
        /// </summary>
        /// <param name="rutaLog">Ruta absoluta de nuestro fichero log.txt.</param>
        /// <remarks>
        /// Este constructor permite crear un logger que se encarga de registrar y escribir 
        /// los eventos ocurridos en nuestra web, en este caso usamos una ruta relativa, 
        /// por eso se le pasa el parámmetro null, también se podría modificar y escribir la ruta absoluta.
        /// </remarks>
        public Logger(string rutaLog = null)
        {
            // Movemos un nivel hacia arriba si el directorio base es "www"
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory; // Ruta actual, es WareHouse10/www
            var rootDirectory = Directory.GetParent(Directory.GetParent(baseDirectory).FullName).FullName; // Guardamos el directorio raiz, es /WareHouse10

            // Definimos la ruta del archivo log en la raíz del proyecto (/WareHouse10)
            rutaLog = rutaLog ?? Path.Combine(rootDirectory, "log.txt");

            // Usa la ruta especificada o, si está vacía, la carpeta Documentos
            _rutaLog = string.IsNullOrEmpty(rutaLog)
                ? System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "log.txt")
                : rutaLog;
        }

        // Método para registrar un evento en el log
        public void RegistrarEvento(string usuario, string evento)
        {
            EntradaLog log = new EntradaLog(usuario, evento);
            _logs.Add(log);

            // Escribir en el archivo de log
            try
            {
                using (var writer = new System.IO.StreamWriter(_rutaLog, true))
                {
                    writer.WriteLine($"{log.FechaEvento} - Usuario: {log.Usuario} - Evento: {log.Evento}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }

        // Devuelve todos los logs
        public List<EntradaLog> ObtenerLogs()
        {
            return _logs;
        }
    }
}
