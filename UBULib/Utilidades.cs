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

//            if (password.Length > 0)
//            {
//                contador++;
//#if DEBUG
//                Console.WriteLine("+1: Mayor a cero");
//#endif
//            }
            if (password.Length <= 16 && password.Length > 0)
            {
                contador =+ 2;
#if DEBUG
                Console.WriteLine("+2: Menor o igual a 16 y mayor de 0.");
#endif
            }
            if (password.Intersect(caracteres).Any())
            {
                contador++;
#if DEBUG
                Console.WriteLine("+1: Detectado letras minúsculas");
#endif
            }
            if (password.Intersect(numeros).Any())
            {
                contador++;
#if DEBUG
                Console.WriteLine("+1: Detectado números.");
#endif
            }
            if (password.Intersect(caracteres.ToUpper()).Any())
            {
                contador++;
#if DEBUG
                Console.WriteLine("+1: Detectado mayúsculas");
#endif
            }
            if (password.Intersect(especiales).Any())
            {
                contador++;
#if DEBUG
                Console.WriteLine("+1: Detectado símbolos especiales");
#endif
            }


#if DEBUG
            Console.WriteLine($"Password: {password}, Count: {contador}");
#endif
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

    }
}
