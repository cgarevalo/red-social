using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio;
using Datos;
using System.Runtime.Remoting.Channels;

namespace Negocio
{
    public class SocialMediaNegocio
    {
        public void CreatePost(Publicacion post)
        {
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.SetearProcedimiento("sp_AltaPost");
				datos.SetearParametro("@idUsuario", post.IdUsuario); 
				datos.SetearParametro("@fecha", post.Fecha); 
				datos.SetearParametro("@idTipoPost", post.IdTipoPublicacion.Id); 
				datos.SetearParametro("@texto", post.Texto); 
				datos.SetearParametro("@idPublicacionCompartida", post.IdPublicacionCompartida);
				datos.EjecutarAccion();

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				datos.CerrarConexion();
			}
        }

		public List<Publicacion> ListarPosts()
		{
			AccesoDatos datos = new AccesoDatos();
			List<Publicacion> posts = new List<Publicacion>();

			try
			{
                datos.SetearProcedimiento("sp_ListarPosts");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
					Publicacion post = new Publicacion();
					post.IdUsuario = (int)datos.Lector["idUsuario"];
					post.NombreUsuario = (string)datos.Lector["nombreUsuario"];
					post.Fecha = (DateTime)datos.Lector["fecha"];

					post.IdTipoPublicacion = new TipoPublicacion();
					post.IdTipoPublicacion.Id = (int)datos.Lector["idTipoPublicacion"];
					post.IdTipoPublicacion.Descripcion = (string)datos.Lector["nombreUsuario"];

                    post.Texto = (string)datos.Lector["texto"];

					if (!(datos.Lector["idPublicacionCompartida"] is DBNull))
						post.IdPublicacionCompartida = (int)datos.Lector["idPublicacionCompartida"];

                    if (!(datos.Lector["fotoPerfil"] is DBNull))
						post.FotoPerfil = (string)datos.Lector["fotoPerfil"];

					posts.Add(post);
                }

                // Verifica si la lista está vacía
                if (posts.Count == 0)
                {
                    // Aquí podrías manejar el caso donde no hay publicaciones
                    // Por ejemplo, podrías retornar un mensaje o manejarlo en la UI
                }

                return posts;
            }
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				datos.CerrarConexion();
			}
		}

		public List<TipoPublicacion> ListarTipoPosts()
		{
			AccesoDatos datos = new AccesoDatos();
			List<TipoPublicacion> tipoPosts = new List<TipoPublicacion>();
            string consulta = "SELECT id, nombre FROM TiposPublicaciones";

            try
			{
				datos.SetearConsulta(consulta);
				datos.EjecutarConsulta();

                while (datos.Lector.Read())
                {
                    TipoPublicacion tipo = new TipoPublicacion()
					{
						Id = (int)datos.Lector["id"],
						Descripcion = (string)datos.Lector["nombre"]
					};

					tipoPosts.Add(tipo);
				}

				return tipoPosts;
            }
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				datos.CerrarConexion();
			}
		}
    }
}
