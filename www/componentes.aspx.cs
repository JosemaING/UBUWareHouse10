using ClassLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBUWHdb;

namespace www
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private WHdb data;
        private Usuario usuarioActual;
        private Logger logger = new Logger();


        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtener el objeto de WHdb y el usuario actual de la sesión
            data = (WHdb)Application["Datos"];
            usuarioActual = (Usuario)Session["User"];

            if (usuarioActual == null)
            {
                // Si el usuario no está en la sesión, redirige a la página de inicio de sesión
                Response.Redirect("inicio.aspx");
            }

            if (!IsPostBack)
            {
                MostrarNotas();
            }

            lblBienvenida.Text = "Bienvenido a tus notas, " + usuarioActual.Email + " id: " + usuarioActual.IdUsuario;
        }

        // Mostrar todas las notas del usuario en el GridView
        private void MostrarNotas()
        {
            if (usuarioActual != null)
            {
                int userId = usuarioActual.IdUsuario;
                var notasUsuario = data.ObtenerComponentesPorUsuario(userId);

                // Enlazar las notas del usuario al GridView
                gvNotas.DataSource = notasUsuario;
                gvNotas.DataBind();
            }
        }

        // Comprueba si reunes los requisitos para seguir creando notas, si eres una cuenta gratuita no puedes crear mas de 3 notas
        private bool ComprobarUsuarioYNumNotas()
        {
            if (usuarioActual.Cuentagratuita && data.ObtenerNumeroDeComponentes(usuarioActual.IdUsuario) < Usuario.CANTIDAD_MAXIMO_PROYECTOS || usuarioActual.EsGestor || !usuarioActual.Cuentagratuita)
            {
                return true;
            }
            return false; // En caso de que no cumpla ninguna de las condiciones
        }

        // Crear una nueva nota y guardarla en WHdb
        protected void btnCrearNota_Click(object sender, EventArgs e)
        {
            string nombre = txtNomElemento.Text;
            string descripcion = txtDesElemento.Text;

            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(descripcion))
            {
                if (ComprobarUsuarioYNumNotas())
                {
                    // Crear el nuevo componente para la nota con el ID del usuario
                    Componente nuevaNota = new Componente(usuarioActual.IdUsuario, nombre, descripcion);

                    // Guardar la nota en WHdb
                    bool notaGuardadaOK = data.GuardaComponente(nuevaNota);
                    if (notaGuardadaOK)
                    {
                        lblMensaje.Text = "Nota creada con éxito.";
                        lblMensaje.Visible = true;
                        lblError.Visible = false;
                        logger.RegistrarEvento(usuarioActual.Email, "Nota creada con éxito.");

                    }
                    MostrarNotas();
                } else
                {
                    lblError.Text = "No puedes crear una nota porque has llegado al límite.";
                    lblError.Visible = true;
                    lblMensaje.Visible = false;
                    logger.RegistrarEvento(usuarioActual.Email, "No puede crear una nota porque ha llegado al límite.");
                }
            }
            else
            {
                lblError.Text = "Por favor, ingresa un nombre y descripción.";
                lblError.Visible = true;
                lblMensaje.Visible = false;
                logger.RegistrarEvento(usuarioActual.Email, "No ha registrado un nombre o descripción para la nota.");

            }
        }

        // Seleccionar una nota en el GridView para edición
        protected void gvNotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gvNotas.SelectedIndex;
            var notasUsuario = data.ObtenerComponentesPorUsuario(usuarioActual.IdUsuario);
            Componente notaSeleccionada = notasUsuario[index];

            txtNomElemento.Text = notaSeleccionada.NomElemento;
            txtDesElemento.Text = notaSeleccionada.DesElemento;

            ViewState["NotaSeleccionada"] = index;
        }

        // Editar una nota seleccionada
        protected void btnEditarNota_Click(object sender, EventArgs e)
        {
            if (ViewState["NotaSeleccionada"] != null)
            {
                int index = (int)ViewState["NotaSeleccionada"];
                var notasUsuario = data.ObtenerComponentesPorUsuario(usuarioActual.IdUsuario);

                // Editar la nota seleccionada en la lista de WHdb
                notasUsuario[index].NomElemento = txtNomElemento.Text;
                notasUsuario[index].DesElemento = txtDesElemento.Text;

                lblMensaje.Text = "Nota editada con éxito.";
                lblMensaje.Visible = true;
                lblError.Visible = false;
                logger.RegistrarEvento(usuarioActual.Email, $"Nota editada correctamente, titulo: {txtNomElemento.Text} con ID: {index}");
                MostrarNotas();
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona una nota para editar.";
                lblMensaje.Visible = true;
                lblError.Visible = false;
                logger.RegistrarEvento(usuarioActual.Email, "No ha seleccionado una nota para su edición");
            }
        }

        // Eliminar una nota seleccionada
        protected void btnEliminarNota_Click(object sender, EventArgs e)
        {
            if (ViewState["NotaSeleccionada"] != null)
            {
                int index = (int)ViewState["NotaSeleccionada"];
                var notasUsuario = data.ObtenerComponentesPorUsuario(usuarioActual.IdUsuario);

                // Eliminar la nota seleccionada de la lista de WHdb
                if (index >= 0 && index < notasUsuario.Count)
                {
                    notasUsuario.RemoveAt(index);
                    lblMensaje.Text = "Nota eliminada con éxito.";
                    logger.RegistrarEvento(usuarioActual.Email, $"Se ha eliminado la nota con ID: {index}");
                    lblMensaje.Visible = true;
                    lblError.Visible = false;
                    MostrarNotas();
                    ViewState["NotaSeleccionada"] = null;
                }
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona una nota para eliminar.";
                logger.RegistrarEvento(usuarioActual.Email, "No ha seleccionado la nota que quiere eliminar");
                lblMensaje.Visible = true;
                lblError.Visible = false;
            }
        }

        // Desconectar, volviendo a la pagina de login
        protected void btnDesconectar_Click(object sender, EventArgs e)
        {
            // Cerrar la sesión del usuario
            Session.Clear();
            Session.Abandon();

            // Redirigir a la página de inicio
            Response.Redirect("inicio.aspx");
        }

        protected void btnPanelControl_Click(object sender, EventArgs e)
        {
            // Redirigir a la página del panel de control
            Response.Redirect("gestionUsuarios.aspx");
        }
    }
}