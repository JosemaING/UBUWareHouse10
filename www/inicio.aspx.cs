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
    public partial class WebForm1 : System.Web.UI.Page
    {
        private WHdb data = null;
        private Usuario usuarioActual = null;
        private Logger logger = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            data = (WHdb)Application["Datos"];
            if (data == null)
            {
                data = new WHdb();
                Application["Datos"] = data;
            }
            usuarioActual = null;

            lblMensaje.Text = "UBU Warehouse Login";
            lblMensaje.Visible = true;
            lblError.Visible = false;
        }

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

            // Inicio de sesión exitoso
            Session["User"] = usuarioActual;
            logger.RegistrarEvento(usuarioActual.Email, "Inicio de sesión exitoso");
            Server.Transfer("componentes.aspx", true);


        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Server.Transfer("registro.aspx", true);
        }
    }
}