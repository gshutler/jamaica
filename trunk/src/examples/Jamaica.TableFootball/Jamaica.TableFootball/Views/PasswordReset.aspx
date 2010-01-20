
<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<PasswordResetResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
    <div>
    
        <h1>Password Reset</h1>
                
        <% using (scope(Xhtml.Form(Resource).ID("passwordResetForm").Method("post"))) { %>
        
            <div class="formElement userSelect">
                <label for="userSelect">User</label>
                <%= Xhtml.Select(() => Resource.UserId, Resource.Users).ID("userSelect")%>
            </div>
                    
            <div class="formElement newPassword">
                <label for="newPassword">New Password</label>
                <%= Xhtml.TextBox(() => Resource.NewPassword).ID("newPassword")%>
            </div>
            
            <div class="formElement formSubmission">
                <button class="submit">Change password</button>
            </div>
            
        <% } %>
                
    </div>
</asp:Content>
