<%@ Page Title="Login" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RedSocial.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3 class="">Log in</h3>

    <div class="row">
        <div class="col-7">
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Ingrese un correo" CssClass="text-danger" ControlToValidate="txtEmail" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="Ingrese un correo válido" CssClass="text-danger" ValidationExpression="^[^@]+@[^@]+\.[a-zA-Z]{2,}$" ControlToValidate="txtEmail" runat="server" />
            </div>

            <div class="mb-3">
                <label for="txtPass" class="form-label">Pass</label>
                <asp:TextBox ID="txtPass" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Ingrese una contraseña" CssClass="text-danger" ControlToValidate="txtPass" runat="server" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblErrorMessage" CssClass="form-label text-danger" runat="server" Text=""></asp:Label>
            </div>

            <asp:Button ID="btnLogIn" CssClass="btn btn-primary" OnClick="btnLogIn_Click" runat="server" Text="Log in" />
            <a href="Default.aspx" class="btn btn-secondary">Cancel</a>
        </div>
    </div>

</asp:Content>
