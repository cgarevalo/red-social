<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="RedSocial.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /*Variables con los IDs de los controles asp para la función previewImage() de js*/
        let fuProfileImageClientId = "<%= fuProfileImage.ClientID %>";
        let imgImagePreviewClientId = "<%= imgImagePreview.ClientID %>";
    </script>
    <%--Enlace al archivo js con la función previewImage()--%>
    <script src="JS/Global/previsualizarImagen.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Sección del encabezado del perfil -->
    <div class="row mb-4 align-items-center mt-2">
        <div class="col-md-3 text-center">
            <!-- Foto de perfil -->

            <asp:Image ID="imgProfile" CssClass="img-fluid" runat="server" />
            <%--<img src="path-to-profile-image.jpg" class="rounded-circle img-fluid" alt="Foto de perfil" style="max-width: 150px;">--%>
        </div>

        <!-- Usuario -->
        <div class="col-md-6">
            <asp:Label ID="lblError" CssClass="form-label text-danger mb-3" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblUserName" CssClass="form-label h2 d-block" runat="server" Text="User name"></asp:Label>
            <button type="button" class="btn btn-outline-primary mt-2" data-bs-toggle="modal" data-bs-target="#editProfileModal">
                Editar Perfil
            </button>

            <!-- Modal -->
            <div class="modal fade" id="editProfileModal" tabindex="-1" aria-labelledby="editProfileLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="editProfileLabel">Editar Perfil</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Formulario para editar perfil -->

                            <!-- Para el nombre de usuario -->
                            <label for="txtName" class="form-label">Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control mb-3" placeholder="Name"></asp:TextBox>

                            <!-- Para cambiar la imagen de perfil -->
                            <label for="fuProfileImage" class="form-label">Profile image</label>
                            <asp:FileUpload ID="fuProfileImage" CssClass="form-control" accept=".jpg,.jpeg,.png,.gif" OnChange="previewImage(fuProfileImageClientId, imgImagePreviewClientId)" runat="server" />
                            <!-- Para ver la imagen seleccionada -->
                            <asp:Image ID="imgImagePreview" CssClass="img-thumbnail img-fluid mt-3" runat="server" />

                            <!-- Agregar otros campos que se quiera editar -->
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Fin modal -->


            <!-- Botón para editar perfil -->
            <%--<a href="EditProfile.aspx" class="btn btn-outline-primary mt-2">Editar perfil</a>--%>
        </div>
        <div class="col-md-3 text-center">
            <!-- Estadísticas del perfil -->
            <ul class="list-unstyled">
                <li><strong>Posts:</strong> 123</li>
                <li><strong>Followers:</strong> 456</li>
                <li><strong>following:</strong> 789</li>
                <li><strong>
                    <asp:Label ID="lblDate" runat="server"></asp:Label></strong> </li>
            </ul>
        </div>
    </div>

    <!-- Sección de información del usuario -->
    <div class="row mb-4">
        <div class="col-md-12">
            <h4>About Me</h4>
            <asp:Label ID="lblAboutMe" CssClass="form-label" runat="server" Text="Esta es la biografía del usuario. Aquí puedes escribir algo sobre ti."></asp:Label>
        </div>
    </div>

    <%--<div class="col-4 d-flex justify-content-start">
            <asp:Image ID="imgPerfil" CssClass="img-fluid" src="https://static.vecteezy.com/system/resources/previews/005/005/788/original/user-icon-in-trendy-flat-style-isolated-on-grey-background-user-symbol-for-your-web-site-design-logo-app-ui-illustration-eps10-free-vector.jpg" runat="server" />
        </div>
        <div class="col-6">
            
        </div>--%>
</asp:Content>
