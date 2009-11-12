<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<PostListResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <h1>Posts</h1>
    <ul>
        <% foreach (var post in Resource.Posts) { %>
        <li>
            <h3><a href="<%= post.CreateUri() %>"><%= post.Title %></a></h3>
            <% if (post.Author != null) { %>
            <p>Written by <%= post.Author.Username %></p>
            <% } %>
            <p><%= post.Description %></p>
            <p>
                <% foreach (var tag in post.Tags) { %>
                <a href="<%= CreateUri.For<TaggedPostsResource>(tag) %>"><%= tag.Name %></a>
                <% } %>
            </p>
        </li>
        <% } %>        
    </ul>
    <ul>
    <% foreach (var pageLink in Resource.PageLinks()) { %>
        <% if (pageLink.Current) { %>
        <li><%= pageLink.Page %></li>
        <% } else if (pageLink.Page == 1) { %>
        <li><a href="<%= CreateUri.For<PostListResource>() %>">1</a></li>
        <% } else { %>
        <li><a href="<%= CreateUri.For<PostListResource>(new{page = pageLink.Page}) %>"><%= pageLink.Page %></a></li>
        <% } %>
    <% } %>
    </ul>
</asp:Content>
