<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<LoginResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Log in</h1>
    <div class="formWrapper loginForm">
    
        <% if (Resource.LoginFailed) { %>
        <p class="error loginFailed">
            <%= Resource.ErrorMessage %>
        </p>        
        <% } %>
    
        <% using (scope(Xhtml.Form(Resource).ID("loginForm").Method("post"))) { %>
        
            <div class="formElement">
                <label for="loginName">Name</label>
                <%= Xhtml.TextBox(() => Resource.Name)
                    .ID("loginName") %>
            </div>
            
            <div class="formElement">    
                <label for="loginPassword">Password</label>
                <%= Xhtml.Password<LoginResource>(user => user.Password)
                    .ID("loginPassword") %>
            </div>
            
            <div class="formElement formSubmission">
                <button class="submit">Log in</button>
            </div>
            
        <% } %>
        
    </div>
</asp:Content>
