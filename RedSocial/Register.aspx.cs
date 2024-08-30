using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dominio;
using Negocio;

namespace RedSocial
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            try
            {
                string email = txtEmail.Text;
                string pass = txtPass.Text;

                if (!String.IsNullOrWhiteSpace(email) || !String.IsNullOrWhiteSpace(pass))
                {
                    // Verifica que pass no supere los 25 y sea igual o mayor a 8 caracteres
                    if (pass.Length < 8)
                    {
                        lblErrorMessage.Text = "La contraseña debe tener una longitud mínima de 8.";
                        return;
                    }
                    else if (pass.Length > 25) 
                    {
                        lblErrorMessage.Text = "La contraseña no debe superar los 25 caracteres.";
                        return;
                    }

                    if (email.Length > 100)
                    {
                        lblErrorMessage.Text = "Email demaciado largo.";
                        return;
                    }

                    // Limpia mensajes previos
                    lblErrorMessage.Text = string.Empty;

                    // Crea el nuevo usuario
                    Usuario newUser = new Usuario
                    {
                        Email = email,
                        Pass = pass,
                        FechaRegistro = DateTime.Now,
                    };

                    UserNegocio negocio = new UserNegocio();

                    // Registra al usuario ya a la vez, obtiene su id 
                    newUser.Id = negocio.RegisterSP(newUser);

                    // Se le asigna un nombre de usuario por defecto usando su id
                    newUser.Nombre = $"carpincho_n°{newUser.Id.ToString()}";
                    
                    // Guarda el nombre
                    negocio.UpdateProfile(newUser);


                    // Redirige a la pagina de login
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    lblErrorMessage.Text = "Debe llenar ambos campos.";
                    return;
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