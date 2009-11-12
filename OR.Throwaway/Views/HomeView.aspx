<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<HomeResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">

    <h1>Welcome</h1>
    
    <% if (User.LoggedIn()) { %>
    <p>Logged in as <%= User.Identity.Name %> :: <a href="<%= CreateUri.For<LogoutResource>() %>">logout</a></p>
    <% } else { %>
    <p>Not logged in :: <a href="<%= CreateUri.For<LoginResource>() %>">login</a></p>
    <% } %>
    
    <p><a href="<%= CreateUri.For<PostListResource>() %>">Latest posts</a></p>
    
    <% if (User.LoggedIn()) { %>
    <p><a href="<%= CreateUri.For<PostCreationResource>() %>">Add post</a></p>
    <% } %>
    
</asp:Content>
