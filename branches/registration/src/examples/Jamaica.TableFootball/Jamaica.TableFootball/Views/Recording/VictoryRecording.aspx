<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<VictoryRecordingResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Record victory</h1>
    <div class="formWrapper victoryRecordForm">
    
        <% if (Resource.SubmissionFailed) { %>
        <p class="error submissionFailed">
            <%= Resource.ErrorMessage %>
        </p>        
        <% } %>
    
        <% using (scope(Xhtml.Form(Resource).ID("victoryRecordForm").Method("post"))) { %>
        
            <div class="formElement opponentSelect">
                <label for="opponentSelect">Beat</label>
                <%= Xhtml.Select(() => Resource.OpponentId, Resource.Opponents).ID("opponentSelect")%>
            </div>
            
            <div class="formElement matchDateSelect">
                <label for="matchDateSelect">When</label>
                <%= Xhtml.Select(() => Resource.MatchDate, Resource.Dates).ID("matchDateSelect")%>
            </div>
        
            <div class="formElement opponentScoreSelect">
                <span class="myScore">10 -</span>
                <%= Xhtml.Select(() => Resource.OpponentScore, Resource.Scores).ID("opponentScoreSelect")%>
            </div>
            
            <div class="formElement formSubmission">
                <button class="submit">Record</button>
            </div>
            
        <% } %>
        
    </div>
</asp:Content>
