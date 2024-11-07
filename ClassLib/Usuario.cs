using System;
using System.Collections.Generic;
using UBULib;

namespace ClassLib
{
    public class Usuario
    {
        // Variables privadas de la clase usuario, definen todas las caracteristicas de un usuario
        private int _idUsuario = -1;
        private string _nombre;
        private string _apellidos;
        private string _email;
        private string _password;
        private bool _esGestor;
        private bool _activo;
        private bool _cuentagratuita;
        private DateTime _caducidad;
        private DateTime _ultimoInicioSesion;
        private DateTime _caducidadPassword;
        private List<String> _passwordAnteriores;
        public const int CANTIDAD_PASSWORD_RECORDADAS = 5;
        public const int CANTIDAD_MAXIMO_PROYECTOS = 3;

        // Nueva instancia de la clase utilidades, se usa para encriptar la password del usuario
        Utilidades utilidades = new Utilidades();

        // Métodos getter y setter para la clase EntradaLog
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public bool EsGestor { get => _esGestor; set => _esGestor = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public bool Cuentagratuita { get => _cuentagratuita; set => _cuentagratuita = value; }
        public DateTime Caducidad { get => _caducidad;}
        public DateTime UltimoInicioSesion { get => _ultimoInicioSesion;}
        public DateTime CaducidadPassword { get => _caducidadPassword; set => _caducidadPassword = value; }
        public List<string> PasswordAnteriores { get => _passwordAnteriores; set => _passwordAnteriores = value; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Usuario"/> con los datos proporcionados.
        /// </summary>
        /// <param name="nombre">Nombre completo del usuario.</param>
        /// <param name="apellidos">Apellidos del usuario.</param>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Contraseña sin encriptar del usuario.</param>
        /// <param name="cuentagratuita">True o False, depende de si el usuario usa cuenta gratuita o no.</param>
        /// <remarks>
        /// Este constructor permite crear nuevas instancias de la clase usuario, 
        /// establece una serie de valores que son usados para identificar y dar distintas distintas funcionalidades.
        /// </remarks>
        public Usuario(string nombre, string apellidos, string email, string password, bool cuentagratuita)
        {
            // Asignamos las variables que definen a un usuario
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellidos = apellidos ?? throw new ArgumentNullException(nameof(apellidos));  
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = utilidades.Encriptar(password);
            EsGestor = false; // De forma predeterminada un usuario no es Gestor
            Activo = true; // De forma predeterminada un nuevo usuario esta activo
            Cuentagratuita = cuentagratuita;
            _caducidad = System.DateTime.Now.AddDays(5); // Nada mas registrar un usuario debe cambiar la contraseña en 5 días
            _ultimoInicioSesion = System.DateTime.MinValue;
            _caducidadPassword = System.DateTime.Now.AddDays(5);
            // Encripta la contraseña y la asigna a Password
            Password = utilidades.Encriptar(password);
            // Inicializa la lista de contraseñas anteriores y agrega la contraseña inicial
            PasswordAnteriores = new List<string> { Password };
        }

        // Metodo que comprueba si la contraseña recibida como argumento es igual a la del usuario, se mantiene un historia de incicios de sesion
        public bool CompruebaPassword(string passwd)
        {
            // Encriptamos la contraseña ingresada por el usuario
            string hashedInput = utilidades.Encriptar(passwd);

            // Comparamos el hash de la entrada con el hash almacenado
            if (_password.Equals(hashedInput))
            {
                // Si coinciden, actualizamos la fecha del último inicio de sesión
                _ultimoInicioSesion = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
