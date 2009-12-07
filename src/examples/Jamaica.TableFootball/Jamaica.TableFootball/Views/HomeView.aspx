
<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<HomeResource>" MasterPageFile="~/Views/HomeView.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
    <div>
    
        <h1>Home</h1>
                
        <ul class="menu">
        <% if (Resource.Authenticated) { %>
            <li>Welcome <span class="userName"><%= Resource.SecurityPrincipal.Name.HtmlEncode() %></span></li>
            <li><%= Xhtml.Link<LogoutResource>()["Log out"] %></li>
            <li><%= Xhtml.Link<VictoryRecordingResource>()["Record victory"] %></li>
        <% } else { %>
            <li class="smallLoginForm">
                <% using (scope(Xhtml.Form<LoginResource>().ID("loginForm").Method("post"))) { %>
                
                    <div class="formElement watermark">
                        <label for="loginName">Name</label>
                        <%= Xhtml.TextBox<LoginResource>(user => user.Name)
                            .ID("loginName") %>
                    </div>
                    
                    <div class="formElement watermark">    
                        <label for="loginPassword">Password</label>
                        <%= Xhtml.Password<LoginResource>(user => user.Password)
                            .ID("loginPassword") %>
                    </div>
                    
                    <div class="formElement formSubmission">
                        <button class="submit">Log in</button>
                    </div>
                    
                <% } %>
            </li>
            <li><%= Xhtml.Link<UserRegistrationResource>()["Register"] %></li>
        <% } %>
        </ul>
        
        <% if (Resource.Authenticated) { %>
        
        <div class="recentResults">
        
            <h3>Recent results</h3>
        
            <ul class="recentResults">
                <% foreach (var result in Resource.RecentResults) { %>
                <li>
                    <span class="verb"><%= result.Verb() %></span>
                    <span class="score"><%= result.UserScore %> - <%= result.OpponentScore %></span>
                    against
                    <span class="opponent"><%= result.OpponentName.HtmlEncode() %></span>
                    <span class="date relativeDate"><%= result.MatchDate.ToJavascriptDateString() %></span>
                </li>
                <% } %>            
            </ul>
        
        </div>
        
        <% } %>
        
        <div class="leagueTable">
        
            <h3>League table</h3>
            
            <ol class="leagueTable">
                <% foreach (var summary in Resource.LeagueTable) { %>
                <li>
                    <dl>
                        <dt>Player</dt>
                        <dd class="user"><%= summary.User.Name.HtmlEncode() %></dd>
                        <dt>Wins</dt>
                        <dd class="wins"><%= summary.Wins %></dd>
                        <dt>Losses</dt>
                        <dd class="losses"><%= summary.Losses %></dd>
                        <dt>Goals scored</dt>
                        <dd class="goalsScored"><%= summary.GoalsScored %></dd>
                        <dt>Goals conceded</dt>
                        <dd class="goalsConceded"><%= summary.GoalsConceded %></dd>
                        <dt>Win percentage</dt>
                        <dd class="winPercentage"><%= summary.WinPercentage %></dd>
                    </dl>
                </li>
                <% } %>
            </ol>        
            
        </div>
        
    </div>
</asp:Content>
