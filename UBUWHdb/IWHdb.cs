using ClassLib;
using System;
using System.Collections.Generic;

namespace UBUWHdb
{
    internal interface IWHdb
    {
        /// Este Interfaz se entrega a modo de requisitos mínimos a implementar y probar.
        /// Debéis de incluir funcionalidades adicionales

        /// <summary>
        /// Almacena el usuario.
        /// </summary>
        /// <param name="u">Objeto de la clase Usuario que se desea almacenar.</param>
        /// <returns>Verdadero o falso en función de si ha conseguido insertar/actualizar la información.</returns>
        bool GuardaUsuario(Usuario u);

        /// <summary>
        /// Lee los datos del usuario que se corresponde con la clave que se recibe como parámetro.
        /// </summary>
        /// <param name="email">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <returns>Retorna el objeto con la infromación del usuario buscado o NULL si no se localiza.</returns>
        Usuario LeeUsuario(String email);

        /// <summary>
        /// Comprueba si el usuario existe existe y el password se corresponde con la almacenada de forma cifrada.
        /// </summary>
        /// <param name="email">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <param name="password">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <returns>Retorna TRUE si los datos de autenticación son válidos.</returns>
        bool ValidaUsuario(string email, string password);

        /// <summary>
        /// Retorna el número de usuarios registrados.
        /// </summary>
        /// <returns>Número de Usuarios.</returns>
        int NumUsuarios();

        /// <summary>
        /// OPCIONAL
        /// Retorna el número de usuarios registrados.
        /// </summary>
        /// <returns>Número de Usuarios.</returns>
        int NumUsuariosActivos();

        /// <summary>
        /// Almacena uno de los componentes que puede ser:
        /// Un espacio principal, subespacio, contenedor o un artículo.
        /// </summary>
        /// <param name="e">Objeto de la clase Componente que se quiere almacenar.</param>
        /// <returns>Verdadero o falso en función de si ha conseguido insertar/ actualizar la información.</returns>
        bool GuardaComponente(Componente e);

        /// <summary>
        /// Lee los datos del elemento referenciado por su ID.
        /// </summary>
        /// <param name="idElemento">Identificador del Componente que se quiere consultar.</param>
        /// <returns>Retorna el objeto con la infromación del conponente buscado o NULL si no se localiza.</returns>
        Componente LeeComponente(int idElemento);

        /// <summary>
        /// Retorna el número de componentes registrados.
        /// </summary>
        /// <returns>Número de Componentes.</returns>
        int NumComponente();

        /// <summary>
        /// Devuelve todos los componentes asignados a ese usuario o una lista vacía si el usuario no tiene componentes
        /// </summary>
        /// <returns>Devuelve todos los componentes asignaos a ese usuario.</returns>
        List<Componente> ObtenerComponentesPorUsuario(int idUsuario);

        /// <summary>
        /// Devuelve todos los usuarios que hay en la base de datos
        /// </summary>
        /// <returns>Devuelve todos los usuarios registrados.</returns>
        List<Usuario> ObtenerTodosLosUsuarios();

        bool DesactivaUsuario(int idUsuario);

        bool CambiaPassword(int idUsuario, string nuevaPassword);

        int ObtenerNumeroDeComponentes(int idUsuario);

        Usuario LeeUsuarioPorId(int idUsuario);

    }
}