<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<UserRegistrationResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
    <h1>Registration</h1>
    <div class="formWrapper registrationForm">
    
        <% using (scope(Xhtml.Form(Resource).ID("registrationForm").Method("post"))) { %>
        
            <div class="formElement">
                <label for="registrationName">Name</label>
                <%= Xhtml.TextBox(() => Resource.Name)
                    .ID("registrationName")
                    .ClassIf(Resource.NameError, "error") %>
                <% if (Resource.NameError) { %>
                <label for="registrationName" class="error">
                    <%= Resource.NameErrorMessage %>
                </label>
                <% } %>
            </div>
            
            <div class="formElement">    
                <label for="registrationPassword">Password</label>
                <%= Xhtml.Password<UserRegistrationResource>(user => user.Password)
                    .ID("registrationPassword")
                    .ClassIf(Resource.PasswordError, "error")%>
                <% if (Resource.PasswordError) { %>
                <label for="registrationPassword" class="error">
                    <%= Resource.PasswordErrorMessage %>
                </label>
                <% } %>
            </div>
                
            <div class="formElement">
                <label for="registrationPasswordConfirmation">Confirm</label>
                <%= Xhtml.Password<UserRegistrationResource>(user => user.PasswordConfirmation)
                    .ID("registrationPasswordConfirmation") %>
            </div>
            
            <div class="formElement formSubmission">
                <input type="submit" class="submit" value="Register" />
            </div>
            
        <% } %>
        
    </div>
</asp:Content>
