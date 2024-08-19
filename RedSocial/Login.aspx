<%@ Page Title="Login" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RedSocial.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3 class="text-light">Login</h3>

    <div class="row">
        <div class="col-4">
            <div class="mb-3">
                <label for="txtEmail" class="form-label text-light">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control bg-dark text-light" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtPass" class="form-label text-light">Pass</label>
                <asp:TextBox ID="txtPass" TextMode="Password" CssClass="form-control bg-dark text-light" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
