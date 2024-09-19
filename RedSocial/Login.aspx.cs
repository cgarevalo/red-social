using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocio;
using Dominio;

namespace RedSocial
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
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
                    lblErrorMessage.Text = string.Empty;

                    UserNegocio negocio = new UserNegocio();
                    Usuario user = new Usuario
                    {
                        Email = email,
                        Pass = pass,
                    };

                    if (negocio.LogIn(user))
                    {
                        Session.Add("userInSession", user);
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        lblErrorMessage.Text = "Contraseña o email son incorrectos.";
                        return;
                    }
                }
                else
                {
                    lblErrorMessage.Text = "Debe llenar ambos campos";
                    return;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx" );
            }
        }
    }
}