<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RedSocial.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Estilos/Clases.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-3 d-flex justify-content-start align-items-center border-end">
            <ul class="list-group flex-column bg-dark">
                <li class="list-group-item bg-dark border-0 w-100">
                    <a class="custom-link" href="Defaul.aspx">Inicio</a>
                </li>
                <li class="list-group-item bg-dark border-0">
                    <a class="custom-link" href="#">Notificaciones</a>
                </li>
                <li class="list-group-item bg-dark border-0">
                    <a class="custom-link" href="#">Mensajes</a>
                </li>
                <li class="list-group-item bg-dark border-0">
                    <a class="custom-link" href="#">Favoritos</a>
                </li>
            </ul>
        </div>

        <%--Centro-------------------------------------------------%>
        <div class="col-6 mt-3 justify-content-center">
            <h3 class="text-light">Buscar</h3>
            <div class="input-group">
                <asp:TextBox ID="txtBuscar" CssClass="form-control bg-dark text-light" placeholder="Buscar" runat="server"></asp:TextBox>
                <asp:Button ID="btnBuscar" CssClass="btn btn-dark border" runat="server" Text="Buscar" />
            </div>

            <%--Create posting--%>
            <div class="mt-3  border p-3">
                <h3 class="text-light">¿Qué estás pensando?</h3>
                <div class="col-12">
                    <asp:TextBox ID="TxtPosteo" CssClass="form-control bg-dark text-light" runat="server"></asp:TextBox>
                    <asp:Button ID="btnPublicar" CssClass="btn btn-dark border mt-3" runat="server" Text="Publicar" />
                </div>
            </div>

            <%--Posts--%>
            <div class="col-12">
                <div class="mb-3">

                </div>
            </div>

        </div>

        <div class="col-3 justify-content-end border-start">
            <h1 class="text-light">hola</h1>
        </div>
    </div>

</asp:Content>
