
<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<HomeResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content" runat="server">
    <div>
        Welcome home.
        
        <ul>
            <li><%= Xhtml.Link<LoginResource>()["Log in"] %></li>
            <li><%= Xhtml.Link<UserRegistrationResource>()["Register"] %></li>
        </ul>
    </div>
</asp:Content>
