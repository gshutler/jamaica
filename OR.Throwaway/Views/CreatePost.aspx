<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<PostCreationResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <h1>Create Post</h1>
    <% if (Errors.Any()) { %>
    <ul class="errors">
    <% foreach (var error in Errors) { %>
        <li><%= error.Message %></li><% } %>
    </ul>
    <% } %>
    <div>        
        <% using (scope(Xhtml.Form(Resource).Method("POST"))) { %>
            <div><%= Xhtml.TextBox(() => Resource.Title) %></div>
            <div><%= Xhtml.TextArea(() => Resource.Description) %></div>
            <div><%= Xhtml.TextBox(() => Resource.TagList) %></div>
            <input type="submit" value="Create" />
        <% } %>
    </div>
    <p>
        <a href="<%= CreateUri.For<PostListResource>() %>">Latest posts</a>
    </p>
</asp:Content>
