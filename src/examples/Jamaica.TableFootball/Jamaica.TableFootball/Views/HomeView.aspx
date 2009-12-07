
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
            
            <table class="leagueTable" cellpadding="0" cellspacing="0">
                <thead>
                    <tr>
                        <th class="position" title="Position"></th>
                        <th class="player" title="Player">Player</th>
                        <th class="played" title="Played">P</th>
                        <th class="won" title="Won">W</th>
                        <th class="lost" title="Lost">L</th>
                        <th class="goalsScored" title="Goals scored">F</th>
                        <th class="goalsConceded" title="Goals conceded">A</th>
                        <th class="winPercentage" title="Win percentage">%</th>
                    </tr>
                </thead>
                <tbody>
                <% var position = 1;
                   foreach (var summary in Resource.LeagueTable) { %>
                    <% if (Resource.Authenticated && summary.User.Name == Resource.SecurityPrincipal.Name) { %>
                    <tr class="currentUser">
                    <% } else { %>
                    <tr>                
                    <% } %>
                        <td class="position"><%= position++ %></td>
                        <td class="player"><%= summary.User.Name.HtmlEncode() %></td>
                        <td class="played"><%= summary.TotalGames %></td>
                        <td class="wins"><%= summary.Wins %></td>
                        <td class="losses"><%= summary.Losses %></td>
                        <td class="goalsScored"><%= summary.GoalsScored %></td>
                        <td class="goalsConceded"><%= summary.GoalsConceded %></td>
                        <td class="winPercentage"><%= summary.WinPercentage.TwoDecimalPlaces() %></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>           
            
        </div>
        
    </div>
</asp:Content>
