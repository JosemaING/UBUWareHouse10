using System.Collections.Generic;
using System;

namespace ClassLib
{
    public class Logger
    {
        private List<EntradaLog> _logs = new List<EntradaLog>();
        private string _rutaLog;

        // Constructor que recibe la ruta como parámetro
        public Logger(string rutaLog = "D:\\Estudios\\Visual Studio Community\\Practica 1\\UBUWhareHouse10\\log.txt")
        {
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
