using ClassLib;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using UBULib;
using UBUWHdb;

namespace www
{
    // Pagina web que premite a un nuevo usuario registrarse para poder acceder a la web
    public partial class Registro : Page
    {
        // Objetos que se usarán, indispensables para el correcto funcionamiento de la pagina
        private WHdb data;
        private Logger logger = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            data = (WHdb)Application["Datos"];
        }

        // Boton que te genera una contrsaeña segura aleatoria
        protected void btnGenerarPassword_Click(object sender, EventArgs e)
        {
            Utilidades utilidades = new Utilidades();
            txtPassword.Text = utilidades.GenerarContrasenaSegura();
            txtPassword.Attributes["value"] = txtPassword.Text;
        }

        // Boton que valida los datos introducidos en el formulario y te registra si todo es correcto 
        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {

            string nombre = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            string email = txtEmail.Text;
            txtPassword.Attributes["value"] = txtPassword.Text;
            string password = txtPassword.Text;

            // Muestra el mail con el que se ha registrado
            lblMensaje.Text = "El email que estás usando es: " + txtEmail.Text;
            lblMensaje.Visible = true;

            // Verifico primero si el email ya existe en la base de datos
            Usuario usuarioExistente = data.LeeUsuario(email);
            if (usuarioExistente != null)
            {
                // Si el usuario ya existe, muestra un mensaje de error
                lblError.Text = "El email ya está registrado. Intente con otro.";
                lblError.Visible = true;
                return;
            }

            // Si el usuario no existe, se procede con el registro
            Usuario nuevoUsuario = new Usuario(nombre, apellidos, email, password, true);
            data.GuardaUsuario(nuevoUsuario);
            logger.RegistrarEvento(email, "Se ha registrado correctamente.");
            // Mensaje de éxito (puedes redirigir a otra página si deseas)
            lblMensaje.Text = "Registro exitoso. Ahora puedes iniciar sesión.";
            lblMensaje.Visible = true;
        }
    }
}
