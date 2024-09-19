using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dominio;
using Negocio;
using System.IO;
using System.Data.SqlClient;

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
                        string rutaProfilesImages = Server.MapPath($"~/Uploads/Images/Profiles/{p.FotoPerfil}");
                        string rutaPostsImages = Server.MapPath($"~/Uploads/Images/Posts/{p.ImagenPublicacion}");
                        string rutaPostsVideos = Server.MapPath($"~/Uploads/Videos/{p.VideoPublicacion}");

                        if (File.Exists(rutaPostsImages))
                            p.ImagenPublicacion = ResolveUrl($"~/Uploads/Images/Posts/{p.ImagenPublicacion}");

                        if (File.Exists(rutaProfilesImages))
                        {
                            p.FotoPerfil = ResolveUrl($"~/Uploads/Images/Profiles/{p.FotoPerfil}");
                        }
                        else
                        {
                            p.FotoPerfil = ResolveUrl("~/Uploads/Images/Profiles/ProfileDefault.jpg");
                        }

                        if (File.Exists(rutaPostsVideos))
                        {
                            p.VideoPublicacion = ResolveUrl($"~/Uploads/Videos/{p.VideoPublicacion}");
                            
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
                    Usuario userLogged = (Usuario)Session["userInSession"];
                    int idTipoPost = int.Parse(ddlTypePost.SelectedValue);
                    int idUser = userLogged.Id;
                    DateTime date = DateTime.Now;
                    int? idPublucacionCopartida = null;
                    string imagenPost;
                    string videoPost;
                    string contenido = null;

                    if (!string.IsNullOrWhiteSpace(txtPosteo.Text))
                        contenido = txtPosteo.Text;


                    switch (idTipoPost)
                    {
                        case 1:
                            lblSuperiorError.Text = string.Empty;
                            string nombreUsuario = userLogged.Nombre;
                            imagenPost = null;
                            videoPost = null;

                            Publicacion newPost = new Publicacion()
                            {
                                IdUsuario = idUser,
                                Fecha = date,
                                Texto = contenido,
                                IdPublicacionCompartida = idPublucacionCopartida,
                                ImagenPublicacion = imagenPost,
                                VideoPublicacion = videoPost
                            };
                            newPost.IdTipoPublicacion = new TipoPublicacion();
                            newPost.IdTipoPublicacion.Id = idTipoPost;

                            negocio.CreatePost(newPost);
                            txtPosteo.Text = string.Empty;
                            break;

                        case 2:
                            if (fuVideosPost.HasFile)
                            {
                                imagenPost = null;
                                string rutaServidor = Server.MapPath("~");

                                // Extensiones de video permitidas
                                string[] extensionesVideos = { ".mp4", ".wmv", ".3gp" };

                                // Obtiene la extensión del video subido
                                string extensionVideo = Path.GetExtension(fuVideosPost.FileName).ToLower();

                                if (extensionesVideos.Contains(extensionVideo))
                                {
                                    lblSuperiorError.Text = string.Empty;

                                    videoPost = Seguridad.Utilidades.SaveVideo(fuVideosPost.PostedFile, rutaServidor);

                                    // Crea el post
                                    Publicacion newPostVideo = new Publicacion()
                                    {
                                        IdUsuario = idUser,
                                        Fecha = date,
                                        Texto = contenido,
                                        IdPublicacionCompartida = idPublucacionCopartida,
                                        ImagenPublicacion = imagenPost,
                                        VideoPublicacion = videoPost
                                    };
                                    newPostVideo.IdTipoPublicacion = new TipoPublicacion();
                                    newPostVideo.IdTipoPublicacion.Id = idTipoPost;

                                    negocio.CreatePost(newPostVideo);
                                }
                                else
                                {
                                    lblSuperiorError.Text = "Solo videos en formato mp4, wmv o 3gp";
                                    return;
                                }
                            }
                            break;

                        case 3:
                            if (fuFotosPost.HasFile)
                            {
                                string rutaServidor = Server.MapPath("~");

                                // Lista de extensiones de imagenes permitidas
                                string[] extensionesImagenes = { ".jpg", ".jpeg", ".png", ".gif", ".jfif", ".webp" };

                                // Obtiene la extensión de la imagen subida
                                string extensionImagen = Path.GetExtension(fuFotosPost.FileName).ToLower();
                                // Verifica si la extensión del archivo subido está en la lista de extensiones permitidas
                                if (extensionesImagenes.Contains(extensionImagen))
                                {
                                    videoPost = null;
                                    lblSuperiorError.Text = string.Empty;

                                    // Guarda la imagen en la carpeta "Posts"
                                    imagenPost = Seguridad.Utilidades.GuardarImagen(fuFotosPost.PostedFile, userLogged, rutaServidor, "Posts");

                                    // Crear el post con la información de la imagen
                                    Publicacion newPostFoto = new Publicacion()
                                    {
                                        IdUsuario = idUser,
                                        Fecha = date,
                                        Texto = contenido,
                                        IdPublicacionCompartida = idPublucacionCopartida,
                                        VideoPublicacion = videoPost,
                                        ImagenPublicacion = imagenPost
                                    };
                                    newPostFoto.IdTipoPublicacion = new TipoPublicacion();
                                    newPostFoto.IdTipoPublicacion.Id = idTipoPost;

                                    negocio.CreatePost(newPostFoto);
                                }
                                else
                                {
                                    lblSuperiorError.Text = "Solo imágenes png, jpg, jpeg, gif, jfif y webp";
                                    return;
                                }

                            }
                            break;
                    }

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