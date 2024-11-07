using ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBULib;
using UBUWHdb;

namespace www
{
    // Pagina web que permite realizar el cambio de contraseña
    public partial class cambioContrasena : System.Web.UI.Page
    {
        // Objetos que se usarán, indispensables para el correcto funcionamiento de la pagina
        private WHdb data;
        private Usuario usuarioActual;
        private Utilidades utilidades = new Utilidades();
        private Logger logger = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            data = (WHdb)Application["Datos"];
            usuarioActual = (Usuario)Session["User"];

            if (usuarioActual == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está en sesión
                Response.Redirect("inicio.aspx");
            }

            if (!IsPostBack)
            {
                // Muestra el email del usuario
                lblEmail.Text = $"Usuario: {usuarioActual.Email}";
            }
        }

        // Al hacer click en Guardar, se cambia a la nueva contraseña y se comprueba si se cumplen todos los criterios de aceptacion
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string contrasenaActual = txtContrasenaActual.Text;
            string nuevaContrasenaEncriptada = utilidades.Encriptar(txtNuevaContrasena.Text);

            // Verifica si la contraseña actual es correcta
            if (usuarioActual.CompruebaPassword(contrasenaActual))
            {
                // Verifica si la nueva contraseña ya ha sido utilizada recientemente
                if (usuarioActual.PasswordAnteriores.Contains(nuevaContrasenaEncriptada))
                {
                    lblMensaje.Text = "La nueva contraseña ya ha sido utilizada recientemente. Por favor, elige una diferente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }

                // Cambia la contraseña a la nueva
                if (data.CambiaPassword(usuarioActual.IdUsuario, nuevaContrasenaEncriptada))
                {
                    lblMensaje.Text = "Contraseña actualizada con éxito.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblSeguridad.Text = "Nivel de dificultad de la contraseña: " + utilidades.VerificaFortalezaPassword(txtNuevaContrasena.Text);
                    lblMensaje.Visible = true;
                    lblSeguridad.Visible = true;

                    logger.RegistrarEvento(usuarioActual.Email, $"Ha cambiado su contraseña con éxito.");
                }
                else
                {
                    lblMensaje.Text = "Hubo un problema al actualizar la contraseña.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                // Muestra un mensaje de error si la contraseña actual es incorrecta
                lblMensaje.Text = "La contraseña actual es incorrecta.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }

        // Al hacer click en el boton Inicio te redirige a la pagina inicial, para hacer de nuevo login
        protected void btnInicio_Click(object sender, EventArgs e)
        {
            // Ir a inico
            Response.Redirect("inicio.aspx");
        }
    }
}