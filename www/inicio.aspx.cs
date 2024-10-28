using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClassLib;
using UBUWHdb;

namespace www
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private WHdb data = null;
        private Usuario usuarioActual = null;
        private bool error = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // if (!Page.IsPostBack)
            //{
                data = (WHdb)Application["Datos"];
                if (data == null)
                {
                    data = new WHdb();
                    Application["Datos"] = data;
                }
                usuarioActual = null;
           // }

            lblMensaje.Text = "UBU Warehouse Login";
            lblMensaje.Visible = true;
            lblError.Visible = false;
        }

        private void InitializeComponent()
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool inicioCorrecto = false;
            usuarioActual = data.LeerUsuario(txtEmail.Text);
            if (usuarioActual != null)
            {
                if (usuarioActual.CompruebaPassword(txtPass.Text))
                {
                    Session["User"] = usuarioActual;
                    inicioCorrecto = true;
                    // Redirigimos a la siguiente pagina
                }
            }

            if (!inicioCorrecto)
            {
                lblError.Text = "Usuario y/o contraseñas incorrectos";
                lblError.Visible = true;
                error = true;
            } else
            {
                Server.Transfer("NewPage.aspx", true);
            }
        }
    }
}