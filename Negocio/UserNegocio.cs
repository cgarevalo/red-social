using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using Dominio;
using Datos;

namespace Negocio
{
    public class UserNegocio
    {
        public int RegisterSP(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Convierte la contraseña en un hash usando SHA256
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Convierte la contraseña a un arreglo de bytes
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Pass);
                    // Obtiene el hash de la contraseña
                    byte[] hashedPassword = sha256.ComputeHash(passwordBytes);

                    datos.SetearProcedimiento("storedAltaUsuario");
                    datos.SetearParametro("@Email", user.Email);
                    datos.SetearParametro("@Pass", hashedPassword);
                    datos.SetearParametro("@Date", user.FechaRegistro);

                    // Ejecuta el procedimiento y devuelve el ID del nuevo usuario
                    return datos.EjecutarAccionScalar();
                }
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

        public bool LogIn(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "SELECT id, email, password, nombre, fotoPerfil, fechaRegistro FROM Usuarios WHERE email = @Email";

            try
            {
                // Convierte la contraseña ingresada en un hash usando SHA256
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Convierte la contraseña a un arreglo de bytes
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Pass);
                    // Obtiene el hash de la contraseña
                    byte[] hashedPassword = sha256.ComputeHash(passwordBytes);

                    datos.SetearConsulta(consulta);
                    datos.SetearParametro("@Email", user.Email);
                    datos.EjecutarLectura();

                    if (datos.Lector.Read())
                    {
                        // Obtiene el hash almacenado en la base de datos
                        byte[] storedPasswordHash = (byte[])datos.Lector["password"];

                        // Compara el hash almacenado con el hash generado y entra de ser verdadero
                        if (hashedPassword.SequenceEqual(storedPasswordHash))
                        {
                            // Obtiene los datos del usuario
                            user.Id = (int)datos.Lector["id"];
                            if (!(datos.Lector["nombre"] is DBNull))
                                user.Nombre = (string)datos.Lector["nombre"];
                            if (!(datos.Lector["fotoPerfil"] is DBNull))
                                user.FotoPerfil = (string)datos.Lector["fotoPerfil"];
                            if (!(datos.Lector["fechaRegistro"] is DBNull))
                                user.FechaRegistro = (DateTime)datos.Lector["fechaRegistro"];

                            return true; // Log in exitoso
                        }
                    }

                    return false; // Log in fallido
                }
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

        public void UpdateProfile(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            string consulta = "UPDATE Usuarios SET nombre = @nombre, fotoPerfil = @fotoPerfil WHERE id = @id";

            try
            {
                datos.SetearConsulta(consulta);
                datos.SetearParametro("@id", user.Id);
                datos.SetearParametro("@nombre", user.Nombre ?? (object)DBNull.Value);
                datos.SetearParametro("@fotoPerfil", user.FotoPerfil ?? (object)DBNull.Value);
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
    }
}
