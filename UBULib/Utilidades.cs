using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UBULib
{
    public class Utilidades{

        // Función para encriptar una cadena de texto.
        public string Encriptar(string password) {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            SHA256 mySHA256 = SHA256.Create();
            bytes = mySHA256.ComputeHash(bytes);

            return (System.Text.Encoding.ASCII.GetString(bytes));
        }

        // Función para comprobar el tipo de complejidad de una contraseña
        public int CompruebaPassword(string password) {


            string caracteres = "abcdefghijklmnñopqrstuvwxyz";
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

    }
}
