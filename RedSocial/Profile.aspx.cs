using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dominio;
using Negocio;
using System.IO;

namespace RedSocial
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Seguridad.Validacion.SesionActiva(Session["userInSession"]))
                {
                    Usuario loggedUser = (Usuario)Session["userInSession"];

                    if (!string.IsNullOrEmpty(loggedUser.Nombre))
                    {
                        lblUserName.Text = loggedUser.Nombre;
                        txtName.Text = loggedUser.Nombre.ToString();
                    }

                    // Carga la imagen de perfil del usuario, o una por defecto si no tiene
                    imgProfile.ImageUrl = Seguridad.Utilidades.GetProfileImage(loggedUser, Server.MapPath("~"));
                    imgImagePreview.ImageUrl = Seguridad.Utilidades.GetProfileImage(loggedUser, Server.MapPath("~"));

                    string fechaRegistro = loggedUser.FechaRegistro?.ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(fechaRegistro))
                        lblDate.Text = $"Joined in: {fechaRegistro}";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                lblError.Text = "Llene el campo";
                return;
            }

            try
            {
                // Elimina cualquier mensaje de error anterior
                lblError.Text = string.Empty;

                if (Seguridad.Validacion.SesionActiva(Session["userInSession"]))
                {
                    UserNegocio negocio = new UserNegocio();
                    Usuario userMod = (Usuario)Session["userInSession"];

                    if (!string.IsNullOrWhiteSpace(name))
                        userMod.Nombre = name;

                    if (fuProfileImage.HasFile)
                    {
                        // Obtiene la extensión del archivo subido
                        string extensionArchivo = Path.GetExtension(fuProfileImage.FileName).ToLower();

                        // Lista de extensiones de imagenes permitidas
                        string[] extensiones = { ".jpg", ".jpeg", ".png", ".gif", ".jfif", ".webp" };

                        // Verifica si la extensión del archivo subido está en la lista de extensiones permitidas
                        if (extensiones.Contains(extensionArchivo))
                        {
                            lblError.Text = string.Empty;

                            // Asigna el nombre de la imagen a UrlImagenPerfil de Usuario y la guarda
                            userMod.FotoPerfil = Seguridad.Utilidades.GuardarImagen(fuProfileImage.PostedFile, userMod, Server.MapPath("~"), "Profile");

                            // Carga la imagen en el imgProfile
                            imgProfile.ImageUrl = Seguridad.Utilidades.GetProfileImage(userMod, Server.MapPath("~"));
                        }
                        else
                        {
                            // Si la extensión no es válida, muestra un mensaje de error y sale
                            lblError.Text = "Solo imágenes png, jpg, jpeg, gif, jfif y webp";
                            return;
                        }
                    }

                    // Actualiza al usuario en sesión
                    Session["userInSession"] = userMod;

                    // Actualiza los datos del usuario
                    negocio.UpdateProfile(userMod);

                    // Redirige a la misma página para que se refleje el cambio en la foto de perfil, sin tener que recargar la página manualmente
                    Response.Redirect("Profile.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }
    }
}