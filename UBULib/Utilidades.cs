using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UBULib
{
    public class Utilidades{

        // Variables para el manejo de intentos fallidos y bloqueo de cuenta
        private int intentosFallidos;
        private const int MaxIntentosFallidos = 5;
        private DateTime? bloqueoHasta;

        // Variables para el manejo del histórico de contraseñas
        private List<string> contrasenasAntiguas = new List<string>();
        private const int MaxHistorico = 5;


        // Función para encriptar una cadena de texto.
        public string Encriptar(string password) {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            SHA256 mySHA256 = SHA256.Create();
            bytes = mySHA256.ComputeHash(bytes);

            return (System.Text.Encoding.ASCII.GetString(bytes));
        }

        // Función para comprobar el tipo de complejidad de una contraseña
        public int CompruebaPassword(string password) {


            string caracteres = "abcdefghijklmnopqrstuvwxyz";
            string numeros = "0123456789";
            string especiales = "[]{}()!¡¿?=.,;:|@#$€+*/_-";

            int contador = 0;

            if (password.Length > 0) contador++;
            if (password.Length < 16) contador++;
            if (password.Intersect(caracteres).Count() > 0) contador++;
            if (password.Intersect(numeros).Count() > 0) contador++;
            if (password.Intersect(caracteres.ToUpper()).Count() > 0) contador++;
            if (password.Intersect(especiales).Count() > 0) contador++;

            return (contador);
        }

        // Función para verificar la fortaleza de una contraseña
        public string VerificaFortalezaPassword(string password)
        {
            int puntaje = CompruebaPassword(password);

            if (puntaje <= 2)
                return "Débil";
            else if (puntaje <= 4)
                return "Moderada";
            else
                return "Fuerte";
        }

        // Función para gestionar intentos fallidos y bloqueo temporal de la cuenta
        public bool IntentoFallido()
        {
            if (bloqueoHasta.HasValue && DateTime.Now < bloqueoHasta.Value)
            {
                return false; // Cuenta bloqueada, no permitir intentos
            }

            intentosFallidos++;

            if (intentosFallidos >= MaxIntentosFallidos)
            {
                bloqueoHasta = DateTime.Now.AddMinutes(15); // Bloqueo por 15 minutos
                return false; // Bloquear cuenta
            }

            return true; // Permitir otro intento
        }

        public void ResetearIntentos()
        {
            intentosFallidos = 0;
            bloqueoHasta = null;
        }

        // Función para generar una contraseña segura
        public string GenerarContrasenaSegura(int longitud = 16)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_-+=[{]};:<>|./?";
            StringBuilder contrasena = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < longitud; i++)
            {
                contrasena.Append(caracteres[random.Next(caracteres.Length)]);
            }

            return contrasena.ToString();
        }

        // Función para verificar y guardar el histórico de contraseñas
        public bool EsContrasenaReutilizada(string nuevaContrasena)
        {
            return contrasenasAntiguas.Contains(nuevaContrasena);
        }

        public void GuardarContrasena(string contrasena)
        {
            if (contrasenasAntiguas.Count >= MaxHistorico)
            {
                contrasenasAntiguas.RemoveAt(0); // Eliminar la más antigua si se supera el límite
            }
            contrasenasAntiguas.Add(contrasena);
        }

    }
}
