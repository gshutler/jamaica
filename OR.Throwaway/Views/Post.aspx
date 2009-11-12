<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<Post>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <h1>Post: <%= Resource.Title %></h1>
    <% if (Resource.Author != null) { %>
    <p>
        Written by <%= Resource.Author.Username %>
        <% if (Resource.Author != null && Resource.Author.Username == User.Identity.Name) { %>
        <a href="<%= Resource.CreateUri("edit") %>">edit this post</a>
        <% } %>
    </p>
    <% } %>    
    <p><%= Resource.Description %></p>
    <p>
        <% foreach (var tag in Resource.Tags) { %>
        <a href="<%= CreateUri.For<TaggedPostsResource>(tag) %>"><%= tag.Name %></a>
        <% } %>
    </p>
    <p>
        <a href="<%= CreateUri.For<PostListResource>() %>">Latest posts</a>
    </p>
</asp:Content>
