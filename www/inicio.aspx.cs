using ClassLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBUWHdb;

namespace www
{
    // Pagina web que controla el inicio de sesion de los usuarios
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Objetos que se usarán, indispensables para el correcto funcionamiento de la pagina
        private WHdb data = null;
        private Usuario usuarioActual = null;
        private Logger logger = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            // En la pagina de inicio si no existe una base de datos se crea de nuevo
            data = (WHdb)Application["Datos"];
            if (data == null)
            {
                data = new WHdb();
                Application["Datos"] = data;
            }
            // Siempre que accedes a la pagina de inicio se cierra la sesión, el usuario es null hasta que inicies
            usuarioActual = null;

            lblMensaje.Text = "UBU Warehouse Login";
            lblMensaje.Visible = true;
            lblError.Visible = false;
        }

        // Boton aceptar, valida los datos de entrada y redirige al panel de componentes
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            usuarioActual = data.LeeUsuario(txtEmail.Text);

            if (usuarioActual == null)
            {
                // Usuario no registrado
                lblError.Text = "Usuario no registrado en el sistema";
                lblError.Visible = true;
                return;
            }

            if (!usuarioActual.CompruebaPassword(txtPass.Text))
            {
                // Contraseña incorrecta
                lblError.Text = "Usuario y/o contraseña incorrectos";
                lblError.Visible = true;
                logger.RegistrarEvento(usuarioActual.Email, "Intento de inicio de sesión con contraseña incorrecta");
                return;
            }

            if (!usuarioActual.Activo)
            {
                // Usuario deshabilitado
                lblError.Text = "Usuario deshabilitado por un gestor";
                lblError.Visible = true;
                logger.RegistrarEvento(usuarioActual.Email, "Intento de inicio de sesión fallido: usuario deshabilitado");
                return;
            }

            // Verifica si la contraseña ha caducado
            if (EsContrasenaCaducada(usuarioActual))
            {
                // Redirige a la página de cambio de contraseña
                Session["User"] = usuarioActual;
                Response.Redirect("cambioContrasena.aspx");
                return;
            }

            // Inicio de sesión exitoso
            Session["User"] = usuarioActual;
            logger.RegistrarEvento(usuarioActual.Email, "Inicio de sesión exitoso");
            Server.Transfer("componentes.aspx", true);


        }

        // Boton que te redirige a la ventana de registro de un nuevo usuario
        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Server.Transfer("registro.aspx", true);
        }

        // Funcion local que comprueba si la contraseña del usuario está caducada
        private bool EsContrasenaCaducada(Usuario usuario)
        {
            return usuario.CaducidadPassword.Date <= DateTime.Today;
        }

    }
}