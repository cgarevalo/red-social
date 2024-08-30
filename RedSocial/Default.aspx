<%@ Page Title="Home" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RedSocial.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        let txtPostClientId = "<%= txtPosteo.ClientID %>";
        let btnPostClientId = "<%= btnCreatePost.ClientID %>";
    </script>
    <script src="JS/default/activar-desactivar-btnCreatePost.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="mt-3 border p-3 rounded shadow-sm mb-3">
        <h3 class="mb-3">¿Qué estás pensando?</h3>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txtPosteo" CssClass="form-control border-0" TextMode="MultiLine" Rows="3" PlaceHolder="Escribe tu publicación..." runat="server" onkeyup="checkInput()"></asp:TextBox>
                <div class="d-flex mt-3">
                    <div class="col-8">
                        <asp:DropDownList ID="ddlTypePost" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="">
                        <asp:Button ID="btnCreatePost" CssClass="btn btn-dark ms-5 rounded-5 w-100" runat="server" OnClick="btnCreatePost_Click" Enabled="false" Text="post" />
                    </div>
                </div>

                <asp:Label ID="lblSuperiorError" runat="server" CssClass="form-label text-danger" Text=""></asp:Label>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
