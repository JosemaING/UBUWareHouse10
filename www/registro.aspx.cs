using ClassLib;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using UBULib;
using UBUWHdb;

namespace www
{
    public partial class Registro : Page
    {

        private WHdb data;
        private Logger logger = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            data = (WHdb)Application["Datos"];
        }

        protected void btnGenerarPassword_Click(object sender, EventArgs e)
        {
            Utilidades utilidades = new Utilidades();
            txtPassword.Text = utilidades.GenerarContrasenaSegura();
            txtPassword.Attributes["value"] = txtPassword.Text;
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {

            string nombre = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            string email = txtEmail.Text;
            txtPassword.Attributes["value"] = txtPassword.Text;
            string password = txtPassword.Text;

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
