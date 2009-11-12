<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<LoginCreationResource>" MasterPageFile="~/Views/HomeView.Master" %>
<%@ Import Namespace="OR.Throwaway.Domain"%>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <h1>Create login</h1>
    
    <% if (Errors.Any()) { %>
    <ul class="errors">
    <% foreach (var error in Errors) { %>
        <li><%= error.Message %></li>
    <% } %>
    </ul>
    <% } %>
    
    <% using (scope(Xhtml.Form(Resource).Method("POST"))) { %>
        <div><%= Xhtml.TextBox(() => Resource.Username) %></div>
        <div><%= Xhtml.Password<LoginCreationResource>(login => login.Password) %></div>
        <div><%= Xhtml.Password<LoginCreationResource>(login => login.PasswordConfirmation) %></div>
        <input type="submit" value="Create login" />
    <% } %>
</asp:Content>
