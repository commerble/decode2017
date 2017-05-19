<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="decodedon.Web.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-3 col-md-offset-3">
        <asp:Login runat="server" LoginButtonStyle-CssClass="btn btn-primary btn-block" />
    </div>
    <div class="col-md-3">
        <asp:CreateUserWizard runat="server" CreateUserButtonStyle-CssClass="account btn btn-primary btn-block" />
    </div>
</asp:Content>
