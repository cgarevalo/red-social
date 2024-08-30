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
        public static string GuardarImagen(HttpPostedFile image, Usuario user, string rutaServidor)
        {
            if (image == null || user == null)
                throw new ArgumentNullException("El archivo de imagen o el usuario no pueden ser nulos.");

            // Obtiene la ruta de la carpeta
            string ruta = Path.Combine(rutaServidor + "/Images/Profiles/");

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
        }

        public static string GetProfileImage(Usuario user, string rutaServidor)
        {
            if (user == null)
                throw new ArgumentNullException("El usuario no puede ser nulo.");

            string ruta = Path.Combine(rutaServidor + "/Images/Profiles/");
            string nombreImagen = string.IsNullOrEmpty(user.FotoPerfil) ? "ProfileDefault.jpg" : user.FotoPerfil;
            string rutaCompleta = Path.Combine(ruta , nombreImagen);

            // Verifica si el archivo existe y devuelve la ruta relativa
            return File.Exists(rutaCompleta) ? $"~/Images/Profiles/{nombreImagen}" : $"~/Images/Profiles/ProfileDefault.jpg";
        }
    }
}
