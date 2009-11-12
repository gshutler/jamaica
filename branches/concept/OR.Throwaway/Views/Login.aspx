<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<LoginResource>" MasterPageFile="~/Views/HomeView.Master" %>
<%@ Import Namespace="OR.Throwaway.Domain"%>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <h1>Login</h1>
    
    <% if (Errors.Any()) { %>
    <ul class="errors">
    <% foreach (var error in Errors) { %>
        <li><%= error.Message %></li>
    <% } %>
    </ul>
    <% } %>
    
    <% using (scope(Xhtml.Form(Resource).Method("POST"))) { %>
        <div><%= Xhtml.TextBox(() => Resource.Username) %></div>
        <div><%= Xhtml.Password<LoginResource>(login => login.Password) %></div>
        <input type="submit" value="Login" />
    <% } %>
    
    <a href="<%= CreateUri.For<LoginCreationResource>() %>">Create new login</a>
</asp:Content>
