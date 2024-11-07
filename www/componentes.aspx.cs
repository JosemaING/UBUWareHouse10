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
    // Pagina web que permite la gestion de componentes
    public partial class WebForm2 : System.Web.UI.Page
    {
        // Objetos que se usarán, indispensables para el correcto funcionamiento de la pagina
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
                MostrarArticulos();
            }

            lblBienvenida.Text = "Bienvenido a tus articulos, " + usuarioActual.Email + " id: " + usuarioActual.IdUsuario;
        }

        // Funcion para mostrar todas los articulos del usuario en el GridView
        private void MostrarArticulos()
        {
            if (usuarioActual != null)
            {
                int userId = usuarioActual.IdUsuario;
                var articulosUsuario = data.ObtenerComponentesPorUsuario(userId);

                // Enlazar los articulos del usuario al GridView
                gvArticulos.DataSource = articulosUsuario;
                gvArticulos.DataBind();
            }
        }

        // Comprueba si reunes los requisitos para seguir creando articulos, si eres una cuenta gratuita no puedes crear mas de 3 articulos
        private bool ComprobarUsuarioYNumArticulos()
        {
            if (usuarioActual.Cuentagratuita && data.ObtenerNumeroDeComponentes(usuarioActual.IdUsuario) < Usuario.CANTIDAD_MAXIMO_PROYECTOS || usuarioActual.EsGestor || !usuarioActual.Cuentagratuita)
            {
                return true;
            }
            return false; // En caso de que no cumpla ninguna de las condiciones
        }

        // Funcion para crear un nuevo articulo y guardarlo en WHdb
        protected void btnCrearArticulo_Click(object sender, EventArgs e)
        {
            string nombre = txtNomElemento.Text;
            string descripcion = txtDesElemento.Text;

            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(descripcion))
            {
                if (ComprobarUsuarioYNumArticulos())
                {
                    // Crear el nuevo componente para el articulo con el ID del usuario
                    Componente nuevoArticulo = new Componente(usuarioActual.IdUsuario, nombre, descripcion);

                    // Guardar el articulo en WHdb
                    bool articuloGuardadoOK = data.GuardaComponente(nuevoArticulo);
                    if (articuloGuardadoOK)
                    {
                        lblMensaje.Text = "Articulo creado con éxito.";
                        lblMensaje.Visible = true;
                        lblError.Visible = false;
                        logger.RegistrarEvento(usuarioActual.Email, "Articulo creado con éxito.");

                    }
                    MostrarArticulos();
                } else
                {
                    lblError.Text = "No puedes crear un articulo porque has llegado al límite.";
                    lblError.Visible = true;
                    lblMensaje.Visible = false;
                    logger.RegistrarEvento(usuarioActual.Email, "No puede crear un articulo porque ha llegado al límite.");
                }
            }
            else
            {
                lblError.Text = "Por favor, ingresa un nombre y descripción.";
                lblError.Visible = true;
                lblMensaje.Visible = false;
                logger.RegistrarEvento(usuarioActual.Email, "No ha registrado un nombre o descripción para el articulo.");

            }
        }

        // Seleccionar un articulo en el GridView para edición
        protected void gvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gvArticulos.SelectedIndex;
            var articulosUsuario = data.ObtenerComponentesPorUsuario(usuarioActual.IdUsuario);
            Componente articuloSeleccionado = articulosUsuario[index];

            txtNomElemento.Text = articuloSeleccionado.NomElemento;
            txtDesElemento.Text = articuloSeleccionado.DesElemento;

            ViewState["ArticuloSeleccionado"] = index;
        }

        // Editar un articulo seleccionada
        protected void btnEditarArticulo_Click(object sender, EventArgs e)
        {
            if (ViewState["ArticuloSeleccionado"] != null)
            {
                int index = (int)ViewState["ArticuloSeleccionado"];
                var articulosUsuario = data.ObtenerComponentesPorUsuario(usuarioActual.IdUsuario);

                // Editar el articulo seleccionada en la lista de WHdb
                articulosUsuario[index].NomElemento = txtNomElemento.Text;
                articulosUsuario[index].DesElemento = txtDesElemento.Text;

                lblMensaje.Text = "Articulo editado con éxito.";
                lblMensaje.Visible = true;
                lblError.Visible = false;
                logger.RegistrarEvento(usuarioActual.Email, $"Articulo editado correctamente, titulo: {txtNomElemento.Text} con ID: {index}");
                MostrarArticulos();
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona un articulo para editar.";
                lblMensaje.Visible = true;
                lblError.Visible = false;
                logger.RegistrarEvento(usuarioActual.Email, "No ha seleccionado un articulo para su edición");
            }
        }

        // Eliminar un articulo seleccionada
        protected void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            if (ViewState["ArticuloSeleccionado"] != null)
            {
                int index = (int)ViewState["ArticuloSeleccionado"];
                var articulosUsuario = data.ObtenerComponentesPorUsuario(usuarioActual.IdUsuario);

                // Eliminar el articulo seleccionada de la lista de WHdb
                if (index >= 0 && index < articulosUsuario.Count)
                {
                    articulosUsuario.RemoveAt(index);
                    lblMensaje.Text = "Articulo eliminado con éxito.";
                    logger.RegistrarEvento(usuarioActual.Email, $"Se ha eliminado el articulo con ID: {index}");
                    lblMensaje.Visible = true;
                    lblError.Visible = false;
                    MostrarArticulos();
                    ViewState["ArticuloSeleccionado"] = null;
                }
            }
            else
            {
                lblMensaje.Text = "Por favor, selecciona un articulo para eliminar.";
                logger.RegistrarEvento(usuarioActual.Email, "No ha seleccionado el articulo que quiere eliminar");
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