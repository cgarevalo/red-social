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
    public partial class Default : System.Web.UI.Page
    {
        SocialMediaNegocio negocio = new SocialMediaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<TipoPublicacion> tipoPosts = negocio.ListarTipoPosts();
                    List<Publicacion> posts = negocio.ListarPosts();

                    // Verifica si tienen imagen de perfil, si no, le asigna una por defecto
                    foreach (Publicacion p in posts)
                    {
                        string rutaImages = Server.MapPath($"~/Images/Profiles/{p.FotoPerfil}");

                        if (File.Exists(rutaImages))
                        {
                            p.FotoPerfil = ResolveUrl($"~/Images/Profiles/{p.FotoPerfil}");
                        }
                        else
                        {
                            p.FotoPerfil = ResolveUrl("~/Images/Profiles/ProfileDefault.jpg");
                        }
                    }

                    // Enlazar los datos al Repeater
                    repPosts.DataSource = posts;
                    repPosts.DataBind();

                    ddlTypePost.DataSource = tipoPosts;
                    ddlTypePost.DataValueField = "Id";
                    ddlTypePost.DataTextField = "Descripcion";
                    ddlTypePost.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnCreatePost_Click(object sender, EventArgs e)
        {
            if (Seguridad.Validacion.SesionActiva(Session["userInSession"]))
            {
                try
                {
                    lblSuperiorError.Text = string.Empty;
                    Usuario userLogged = (Usuario)Session["userInSession"];

                    string contenido = txtPosteo.Text;
                    int idUser = userLogged.Id;
                    string nombreUsuario = userLogged.Nombre;
                    DateTime date = DateTime.Now;
                    int? idPublucacionCopartida = null;
                    int idTipoPost = int.Parse(ddlTypePost.SelectedValue);

                    Publicacion newPost = new Publicacion()
                    {
                        IdUsuario = idUser,
                        Fecha = date,
                        Texto = contenido,
                        IdPublicacionCompartida = idPublucacionCopartida
                    };

                    newPost.IdTipoPublicacion = new TipoPublicacion();
                    newPost.IdTipoPublicacion.Id = idTipoPost;

                    negocio.CreatePost(newPost);
                    txtPosteo.Text = string.Empty;
                    Response.Redirect("Default.aspx", false);
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.Message);
                    Response.Redirect("Error.aspx");
                }
            }
            else
            {
                txtPosteo.Text = string.Empty;
                Response.Redirect("Login.aspx", false);
                return;
            }
        }
    }
}