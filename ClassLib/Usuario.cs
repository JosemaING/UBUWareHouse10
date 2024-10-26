using UBULib;

namespace ClassLib
{
    public class Usuario
    {
        private string _email;
        private string _nombre;
        private string _apellidos;
        private string _password;

        Utilidades utilidades = new Utilidades();

        public Usuario(string email, string nombre, string apellidos, string password)
        {
            Email = email;
            Nombre = nombre;
            Apellidos = apellidos;
            _password = utilidades.Encriptar(password);
        }

        public string Email { get => _email; set => _email = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }



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
