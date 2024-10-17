<%@ Page Title="Home" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RedSocial.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /* Variables para la activar y desactivar el botón de posteo */
        let txtPostClientId = "<%= txtPosteo.ClientID %>";
        let btnPostClientId = "<%= btnCreatePost.ClientID %>";
        let fuFotosPostClientId = "<%= fuFotosPost.ClientID %>";
        let fuVideosPostClientId = "<%= fuVideosPost.ClientID %>";
        let vidPreviewClientId = "<%= vidPreview.ClientID %>";
        let vidContainerClientId = "<%= vidContainer.ClientID %>";

        // variable para cargar la foto seleccinada
        let imgFotoPostClientId = "<%= imgFotoPost.ClientID %>";
        let btnRemoveImageClientId = "<%= btnRemoveImage.ClientID %>";
        let btnRemoveVideoClientId = "<%= btnRemoveVideo.ClientID %>";

        /* Variables para la subida de archivos de los FileLoads */
        let ddlTypePostClientId = "<%= ddlTypePost.ClientID %>";
        let divFuFotosClientId = "<%= divFuFotos.ClientID %>";
        let divFuVideosClientId = "<%= divFuVideos.ClientID %>";
    </script>
    <script src="JS/default/activar-desactivar-btnCreatePost.js"></script>
    <script src="JS/default/mostrar-FileLoad.js"></script>
    <script src="JS/Global/previsualizarImagen.js"></script>
    <script src="JS/default/Limpiar.js"></script>
    <script src="JS/Global/previsualizarVideo.js"></script>
    <script src="JS/Global/limpiarVideo.js"></script>

    <link href="Estilos/Limites.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

    <div class="mt-3 border p-3 rounded shadow-sm mb-3">
        <h3 class="mb-3">¿Qué estás pensando?</h3>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:TextBox ID="txtPosteo" CssClass="form-control border-0" TextMode="MultiLine" Rows="3" PlaceHolder="Escribe tu publicación..." runat="server" onkeyup="checkInput()"></asp:TextBox>

                <div class="d-flex mt-3">
                    <div class="col-8">
                        <asp:DropDownList ID="ddlTypePost" CssClass="form-control" runat="server" onchange="toggleFileUpload()"></asp:DropDownList>
                    </div>

                    <div class="">
                        <asp:Button ID="btnCreatePost" CssClass="btn btn-dark ms-5 rounded-5 w-100" runat="server" OnClick="btnCreatePost_Click" Enabled="false" Text="post" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Controles de subida de archivos -->
        <div id="divFuFotos" class="mt-3 col-8" style="display: none;" runat="server">
            <label class="form-label ms-1" for="fuFotosPost">Select foto</label>
            <asp:FileUpload ID="fuFotosPost" CssClass="form-control" runat="server" onchange="checkInput(), previewImage(fuFotosPostClientId, imgFotoPostClientId)" accept=".jpg,.jpeg,.png,.gif,.jfif,.webp" />
        </div>
        <div id="divFuVideos" class="mt-3 col-8" style="display: none;" runat="server">
            <label class="form-label ms-1" for="fuVideosPost">Select video</label>
            <asp:FileUpload ID="fuVideosPost" CssClass="form-control" runat="server" onchange="checkInput(), previewVideo(fuVideosPostClientId, vidContainerClientId, vidPreviewClientId)" accept=".mp4, .wmv, .3gp" />
        </div>

        <!-- Previsualización de video -->
        <div id="vidContainer" class="preview-container" style="display: none;" runat="server">
            <video id="vidPreview" class="mt-3 video-preview" controls style="max-width: 100%; height: auto;" runat="server">
                <source id="vidSource" type="video/mp4" />
            </video>
            <!-- Botón "X" para eliminar la previsualización del video -->
            <button id="btnRemoveVideo" type="button" class="btn-close position-absolute " aria-label="Close" onclick="clearVideoPreview()" style="display: none; top: 3px; right: 3px; z-index: 10;" runat="server"></button>
        </div>

        <!-- Previsualización de imagen -->
        <div id="imgContainer" class="position-relative d-inline-block">
            <asp:Image ID="imgFotoPost" CssClass="img-preview mt-3" runat="server" />

            <!-- Botón "X" para eliminar la previsualización de la imagen -->
            <button type="button" class="btn-close position-absolute" aria-label="Close" onclick="clearImagePreview()" style="display: none; top: 20px; right: 3px; z-index: 10;" id="btnRemoveImage" runat="server"></button>
        </div>

        <asp:Label ID="lblSuperiorError" runat="server" CssClass="form-label text-danger" Text=""></asp:Label>
    </div>

    <asp:UpdatePanel ID="upPosts" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="repPosts" runat="server">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="row g-0">
                            <div class="d-flex">
                                <asp:Image CssClass="img-fluid" Style="height: 50px; width: 50px; border-radius: 50%; background-color: gray" runat="server" ImageUrl='<%# Eval("FotoPerfil") %>' />
                                <p class="card-text p-2">
                                    <small class="text-muted"><strong class="me-2"><%# Eval("NombreUsuario") %> </strong><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></small>
                                </p>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="card-body">

                                <!-- Texto de la publicación -->
                                <p class="card-text"><%# Eval("Texto") %></p>

                                <!-- Mostrar la imagen de la publicación, si existe -->
                                <asp:Image ID="imgPublicacion" CssClass="img-fluid img-preview mt-3" runat="server" ImageUrl='<%# Eval("ImagenPublicacion") %>' />

                                <!-- Mostrar el video de la publicación, si existe -->
                                <div id="vidPublicacion" class="preview-container" runat="server" visible='<%# Eval("VideoPublicacion") != null && Eval("VideoPublicacion").ToString() != "" %>'>
                                    <video controls style="max-width: 100%; height: auto;">
                                        <source src='<%# Eval("VideoPublicacion") %>' type="video/mp4" />
                                    </video>
                                </div>

                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
