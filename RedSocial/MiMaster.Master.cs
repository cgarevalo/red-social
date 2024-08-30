using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dominio;
using System.IO;

namespace RedSocial
{
    public partial class MiMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.Validacion.SesionActiva(Session["userInSession"]))
            {
                // Si hay una sesión activa, obtiene los datos del usuario de la sesión y configura la interfaz de usuario
                Usuario user = (Usuario)Session["userInSession"];

                // Establece el nombre de usuario en la label, si tiene
                if (!String.IsNullOrEmpty(user.Nombre))
                    lblUser.Text = user.Nombre;

                imgprofileNav.ImageUrl = Seguridad.Utilidades.GetProfileImage(user, Server.MapPath("~"));
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}