<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<Post>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <h1>Post: <%= Resource.Title %></h1>
    <% if (Errors.Any()) { %>
    <ul class="errors">
    <% foreach (var error in Errors) { %>
        <li><%= error.Message %></li><% } %>
    </ul>
    <% } %>
    <div>        
        <% using (scope(Xhtml.Form(Resource).Action(Resource.CreateUri("edit")).Method("POST"))) { %>
            <div><%= Xhtml.TextBox(() => Resource.Title) %></div>
            <div><%= Xhtml.TextArea(() => Resource.Description) %></div>
            <div><input type="text" name="tagList" value="<%= Resource.TagList() %>" /></div>
            <input type="submit" value="Update" />
        <% } %>
    </div>
    <div>
        <% using (scope(Xhtml.Form(Resource).Method("DELETE"))) { %>
            <input type="submit" value="Delete" />
        <% } %>
    </div>
    <p>
        <a href="<%= CreateUri.For<PostListResource>() %>">Latest posts</a>
    </p>
</asp:Content>
