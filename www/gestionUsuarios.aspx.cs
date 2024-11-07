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
    // Pagina web que permite la gestion de usuarios, depende del tipo de usuario que seas tiene unos permisos u otros
    public partial class gestionUsuarios : System.Web.UI.Page
    {
        // Objetos que se usarán, indispensables para el correcto funcionamiento de la pagina
        private WHdb data;
        Utilidades u = new Utilidades();
        private Logger logger = new Logger();


        protected void Page_Load(object sender, EventArgs e)
        {
            data = (WHdb)Application["Datos"];

            // Obtener el usuario actual desde la sesión
            Usuario usuarioActual = (Usuario)Session["User"];

            if (usuarioActual == null)
            {
                // Redirigir al inicio de sesión si no hay un usuario en la sesión
                Response.Redirect("inicio.aspx");
                return;
            }

            if (!IsPostBack)
            {
                MostrarUsuarios(usuarioActual);
            }
        }

        // Muestra todos los usuarios si eres gestor, sino solo muestra tu usuario
        private void MostrarUsuarios(Usuario usuarioActual)
        {
            List<Usuario> listaUsuarios;

            if (usuarioActual.EsGestor)
            {
                // Si el usuario es gestor, mostrar todos los usuarios
                listaUsuarios = new List<Usuario>(data.ObtenerTodosLosUsuarios());
            }
            else
            {
                // Si no es gestor, mostrar solo su propio usuario
                listaUsuarios = new List<Usuario> { usuarioActual };
            }

            gvUsuarios.DataSource = listaUsuarios;
            gvUsuarios.DataBind();
        }

        // Boton para crear un nuevo usuario, solo si eres gestor
        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Usuario usuarioActual = (Usuario)Session["user"];

            // Verificar si el usuario actual es gestor
            if (usuarioActual == null || !usuarioActual.EsGestor)
            {
                lblError.Text = "No tienes permisos para crear usuarios.";
                logger.RegistrarEvento(usuarioActual.Email, "Ha intentado crear un usuario, pero no tiene permisos");
                lblError.Visible = true;
                lblMensaje.Visible = false;
                return; // Detener la ejecución si no es gestor
            }

            var usuario = new Usuario(
                txtNombre.Text,
                txtApellidos.Text,
                txtEmail.Text,
                u.Encriptar(txtPassword.Text),
                chkCuentaGratuita.Checked
            )
            {
                Activo = chkActivo.Checked,
                EsGestor = chkEsGestor.Checked,
                CaducidadPassword = DateTime.Now.AddMonths(2) // Establece la caducidad a 2 meses
            };

            bool guardado = data.GuardaUsuario(usuario);
            if (guardado)
            {
                lblMensaje.Text = "Usuario creado exitosamente.";
                logger.RegistrarEvento(usuarioActual.Email, "Usuario creado correctamente.");
                lblMensaje.Visible = true;
                lblError.Visible = false;
                MostrarUsuarios((Usuario)Session["user"]);
            }
            else
            {
                lblError.Text = "Error al crear el usuario.";
                logger.RegistrarEvento(usuarioActual.Email, "Error al insertar el usuario a la BBDD");
                lblError.Visible = true;
                lblMensaje.Visible = false;
            }

        }

        // Boton para crear editar un usuario, depende tu rango puedes editar solo el tuyo o el del resto
        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            if (ViewState["UsuarioSeleccionado"] != null)
            {
                int idUsuario = (int)ViewState["UsuarioSeleccionado"];
                Usuario usuario = data.LeeUsuarioPorId(idUsuario);

                usuario.Nombre = txtNombre.Text;
                usuario.Apellidos = txtApellidos.Text;
                usuario.Email = txtEmail.Text;

                // Obtener el usuario actual desde la sesión
                Usuario usuarioActual = (Usuario)Session["user"];

                // Si el usuario actual es gestor, permitirle modificar estos campos
                if (usuarioActual.EsGestor)
                {
                    // Verificar si se ingresó una nueva contraseña
                    if (!string.IsNullOrEmpty(txtPassword.Text))
                    {
                        // Encriptar y asignar la nueva contraseña
                        usuario.Password = u.Encriptar(txtPassword.Text);
                        // Actualizar la caducidad de la contraseña a 2 meses desde hoy
                        usuario.CaducidadPassword = DateTime.Now.AddMonths(2);
                    }
                    else
                    {
                        // Si no se ingresó una nueva contraseña, mantener la contraseña actual
                        usuario.Password = Server.HtmlDecode(hdnPassword.Value);
                        // No se actualiza la caducidad de la contraseña
                    }

                    usuario.EsGestor = chkEsGestor.Checked;
                    usuario.Activo = chkActivo.Checked;
                    usuario.Cuentagratuita = chkCuentaGratuita.Checked;
                }

                if (data.GuardaUsuario(usuario))
                {
                    lblMensaje.Text = "Usuario editado con éxito.";
                    logger.RegistrarEvento(usuarioActual.Email, $"Usuario editado: {txtEmail.Text}");
                    lblMensaje.Visible = true;
                    lblError.Visible = false;
                    MostrarUsuarios(usuarioActual);
                }
                else
                {
                    lblError.Text = "Error al editar el usuario.";
                    logger.RegistrarEvento(usuarioActual.Email, "Error al modificar el usuario en la BBDD");
                    lblError.Visible = true;
                    lblMensaje.Visible = false;
                }
            }
        }

        // Boton para eliminar un usuario de la base de datos
        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            // Verificar si el usuario actual es gestor
            Usuario usuarioActual = (Usuario)Session["user"];

            if (!usuarioActual.EsGestor)
            {
                // Mostrar un mensaje de error si el usuario no tiene permiso para eliminar
                lblError.Text = "No tienes permisos para eliminar usuarios.";
                lblError.Visible = true;
                lblMensaje.Visible = false;
                return;
            }

            if (ViewState["UsuarioSeleccionado"] != null)
            {
                int idUsuario = (int)ViewState["UsuarioSeleccionado"];
                bool eliminado = data.EliminarUsuario(idUsuario);

                if (eliminado)
                {
                    lblMensaje.Text = "Usuario eliminado con éxito.";
                    logger.RegistrarEvento(usuarioActual.Email, $"Usuario eliminado: {txtEmail.Text}");
                    lblMensaje.Visible = true;
                    lblError.Visible = false;
                    MostrarUsuarios(usuarioActual); // Actualizar la lista después de la eliminación
                    ViewState["UsuarioSeleccionado"] = null;
                }
                else
                {
                    lblError.Text = "Error al eliminar el usuario.";
                    lblError.Visible = true;
                    lblMensaje.Visible = false;
                }
            }
        }

        // Fila con los datos del usuario seleccionado que se quiere editar
        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = gvUsuarios.SelectedIndex;
            int idUsuario = (int)gvUsuarios.DataKeys[index].Value;

            Usuario usuario = data.LeeUsuarioPorId(idUsuario);
            if (usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                txtApellidos.Text = usuario.Apellidos;
                txtEmail.Text = usuario.Email;
                hdnPassword.Value = Server.HtmlEncode(usuario.Password);
                txtPassword.Text = string.Empty;
                chkEsGestor.Checked = usuario.EsGestor;
                chkActivo.Checked = usuario.Activo;
                chkCuentaGratuita.Checked = usuario.Cuentagratuita;

                ViewState["UsuarioSeleccionado"] = idUsuario;

                // Obtener el usuario actual desde la sesión para verificar permisos
                Usuario usuarioActual = (Usuario)Session["user"];

                // Si el usuario actual no es gestor, deshabilitar los checkboxes
                if (!usuarioActual.EsGestor)
                {
                    chkEsGestor.Enabled = false;
                    chkActivo.Enabled = false;
                    chkCuentaGratuita.Enabled = false;
                    txtPassword.Enabled = false;
                }
                else
                {
                    chkEsGestor.Enabled = true;
                    chkActivo.Enabled = true;
                    chkCuentaGratuita.Enabled = true;
                    txtPassword.Enabled = true;
                }
            }
        }

        // Desconectar, volviendo a la pagina de login
        protected void btnDesconectar_Click(object sender, EventArgs e)
        {
            // Obtener el usuario actual desde la sesión
            Usuario usuarioActual = (Usuario)Session["User"];
            logger.RegistrarEvento(usuarioActual.Email, $"Se ha desconectado.");

            // Cerrar la sesión del usuario
            Session.Clear();
            Session.Abandon();

            // Redirigir a la página de inicio
            Response.Redirect("inicio.aspx");
        }

        protected void btnArticulos_Click(object sender, EventArgs e)
        {
            // Redirigir a la página del panel de articulos
            Response.Redirect("componentes.aspx");
        }

        // Redirige a la página de cambio de contraseña
        protected void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            Response.Redirect("cambioContrasena.aspx");
        }
    }
}