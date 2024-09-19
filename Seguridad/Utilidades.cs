using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio;
using System.Web;
using System.IO;

namespace Seguridad
{
    public static class Utilidades
    {
        public static string GuardarImagen(HttpPostedFile image, Usuario user, string rutaServidor, string subCarpeta)
        {
            if (image == null || user == null)
                throw new ArgumentNullException("El archivo de imagen o el usuario no pueden ser nulos.");

            // Obtiene la ruta de la carpeta
            string ruta = Path.Combine($"{rutaServidor}/Uploads/Images/{subCarpeta}");

            switch (subCarpeta)
            {
                case "Profile":
                    // Verifica si el usuario ya tiene una imagen de perfil
                    if (!String.IsNullOrEmpty(user.FotoPerfil))
                    {
                        // Ruta completa de la imagen antigua
                        string rutaImgAntigua = Path.Combine(ruta, user.FotoPerfil);

                        if (File.Exists(rutaImgAntigua))
                        {
                            try
                            {
                                // Elimina la imagen antigua si existe
                                File.Delete(rutaImgAntigua);
                            }
                            catch (Exception ex)
                            {
                                throw new IOException("Error al eliminar la imagen antigua.", ex);
                            }
                        }
                    }
                    // Generar un nombre único para la imagen
                    string nombreImagen = $"usuario-{user.Id.ToString()}-.jpg";

                    // Combina la ruta de la imagen con el nombre
                    string rutaCompleta = Path.Combine(ruta, nombreImagen);

                    try
                    {
                        // Guarda la imagen
                        image.SaveAs(rutaCompleta);
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Error al guardar la imagen.", ex);
                    }

                    return nombreImagen;

                case "Posts":
                    // Generar un nombre único para la imagen del posteo
                    string nombreImagenPost = $"post-{Guid.NewGuid()}.jpg";

                    // Combina la ruta de la imagen con el nombre
                    string rutaCompletaPost = Path.Combine(ruta, nombreImagenPost);

                    try
                    {
                        // Guarda la imagen
                        image.SaveAs(rutaCompletaPost);
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Error al guardar la imagen del posteo.", ex);
                    }
                    return nombreImagenPost;

                default:
                    throw new ArgumentException("Subcarpeta no válida.");
            }
        }

        public static string SaveVideo(HttpPostedFile video, string rutaServidor)
        {
            if (video == null)
                throw new ArgumentNullException("El archivo de video no puede ser nulo.");

            string ruta = Path.Combine($"{rutaServidor}/Uploads/Videos/");
            string nombreVideo = $"video-{Guid.NewGuid()}.mp4";
            string rutaCompleta = Path.Combine(ruta, nombreVideo);

            try
            {
                video.SaveAs(rutaCompleta);
            }
            catch (Exception ex)
            {
                throw new IOException("Error al guardar el video.", ex);
            }

            return nombreVideo;
        }

        public static string GetProfileImage(Usuario user, string rutaServidor)
        {
            if (user == null)
                throw new ArgumentNullException("El usuario no puede ser nulo.");

            string ruta = Path.Combine(rutaServidor + "/Uploads/Images/Profiles/");
            string nombreImagen = string.IsNullOrEmpty(user.FotoPerfil) ? "ProfileDefault.jpg" : user.FotoPerfil;
            string rutaCompleta = Path.Combine(ruta, nombreImagen);

            // Verifica si el archivo existe y devuelve la ruta relativa
            return File.Exists(rutaCompleta) ? $"~/Uploads/Images/Profiles/{nombreImagen}" : $"~/Uploads/Images/Profiles/ProfileDefault.jpg";
        }
    }
}
