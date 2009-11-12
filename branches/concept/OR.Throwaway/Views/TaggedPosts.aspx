<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<TaggedPostsResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <h1>Posts tagged with "<%= Resource.Tag.Name %>"</h1>
    <ul>
        <% foreach (var post in Resource.TaggedPosts) { %>
        <li>
            <h3><a href="<%= post.CreateUri() %>"><%= post.Title %></a></h3>
            <p><%= post.Description %></p>
            <p>
                <% foreach (var tag in post.Tags) { %>
                <a href="<%= CreateUri.For<TaggedPostsResource>(tag) %>"><%= tag.Name %></a>
                <% } %>
            </p>
        </li>
        <% } %>
    </ul>
</asp:Content>
