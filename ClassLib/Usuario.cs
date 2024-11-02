using System;
using System.Collections.Generic;
using UBULib;

namespace ClassLib
{
    public class Usuario
    {
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
        //private List<Elemento> espacios = new List<Elemento>();

        Utilidades utilidades = new Utilidades();

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

        public Usuario(string nombre, string apellidos, string email, string password, bool cuentagratuita)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellidos = apellidos ?? throw new ArgumentNullException(nameof(apellidos));  
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = utilidades.Encriptar(password);
            EsGestor = false;
            Activo = true;
            Cuentagratuita = cuentagratuita;
            _caducidad = System.DateTime.Now;
            _ultimoInicioSesion = System.DateTime.MinValue;
            _caducidadPassword = System.DateTime.Now;
            PasswordAnteriores = new List<String>();
        }

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
