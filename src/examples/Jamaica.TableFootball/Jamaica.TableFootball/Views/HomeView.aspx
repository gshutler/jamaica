
<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<HomeResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
    <div>
    
        <h1>Home</h1>
        
        <ul>
        <% if (Resource.Authenticated) { %>
            <li>Welcome <strong><%= Resource.SecurityPrincipal.Name %></strong></li>
            <li><%= Xhtml.Link<LogoutResource>()["Log out"] %></li>
        <% } else { %>
            <li class="loginForm">
                <% using (scope(Xhtml.Form<LoginResource>().ID("loginForm").Method("post"))) { %>
                
                    <div class="formElement">
                        <label for="loginName">Name</label>
                        <%= Xhtml.TextBox<LoginResource>(user => user.Name)
                            .ID("loginName") %>
                    </div>
                    
                    <div class="formElement">    
                        <label for="loginPassword">Password</label>
                        <%= Xhtml.Password<LoginResource>(user => user.Password)
                            .ID("loginPassword") %>
                    </div>
                    
                <% } %>
            </li>
            <li><%= Xhtml.Link<UserRegistrationResource>()["Register"] %></li>
        <% } %>
        </ul>
        
    </div>
</asp:Content>
