using UBULib;

namespace ClassLib
{
    public class Usuario
    {
        private int _idUsuario;
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
        private const int CANTIDAD_PASSWORD_RECORDADAS = 5;
        private const int CANTIDAD_MAXIMO_PROYECTOS = 3;
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
        public DateTime CaducidadPassword { get => _caducidadPassword;}
        public List<string> PasswordAnteriores { get => _passwordAnteriores; set => _passwordAnteriores = value; }

        public Usuario(string nombre, string apellidos, string email, string password, bool cuentagratuita)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellidos = apellidos ?? throw new ArgumentNullException(nameof(apellidos));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            EsGestor = false;
            Activo = false;
            Cuentagratuita = cuentagratuita;
            _caducidad = System.DateTime.Now;
            _ultimoInicioSesion = System.DateTime.MinValue;
            _caducidadPassword = System.DateTime.Now;
            PasswordAnteriores = new List<String>();
        }



        public override bool Equals(object? obj)
        {
            return obj is Usuario usuario &&
                   Email == usuario.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email);
        }
    }
}
