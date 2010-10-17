<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TodayModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Daily Plan for <%= Model.FirstName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="header">    
    <div class="heading">
        <ul class="sf-menu">
			<li class="current">
				<a href="#"> Menu </a>
				<ul>
                    <li>
                    <%= Html.ActionLink("About", "Index", "About") %>
                    </li>
					<li>
						<%= Html.ActionLink("Copy Forward", "CopyForward", Model.DayAsRouteValues())%>
					</li>
					<li>
						<%= Html.ActionLink("Sign Out", "SignOut", "Auth") %>
					</li>					
				</ul>
			</li>	
		</ul>
        
    </div>
    <div class="heading">
        <%= Html.ActionLink("<<", "BackADay", Model.DayAsRouteValues()) %>
         &nbsp; Daily Plan: <%= Model.AccountDay.ToLongDateString() %> &nbsp; 
         <%= Html.ActionLink(">>", "ForwardADay", Model.DayAsRouteValues()) %></div>
</div>

<div class="content">
	<div class="lines">		
<% foreach (Goal g in Model.Goals) { %>
        <div goalid="<%= g.Id %>" class="line full <%= g.Done ? "done" : "" %>"><%= g.Description %>
            <img src="<%= Url.Content("~/Content/Images/delete.png") %>" alt="Delete" class="delete" /><input type="checkbox" <%= g.Done ? "checked='checked'" : "" %> class="toggle" />
        </div>
<% } %>
		<div class="line">            
			<form class="goal_form">
                <input type="text" id="goal" name="goal" class="goal"/>
                <%= Html.HiddenFor(m => m.NameSlug) %>
                <input type="hidden" name="year" value="<%=Model.AccountDay.Year %>" />
                <input type="hidden" name="month" value="<%=Model.AccountDay.Month %>" />
                <input type="hidden" name="day" value="<%=Model.AccountDay.Day %>" />
            </form>
		</div>	
		<div class="line"></div>		
		<div class="line"></div>		
		<div class="line"></div>		
	</div>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $('ul.sf-menu').superfish();    
    });


$($('.goal')[0]).focus();

$('input.toggle').live('change', function () {
    var container = $(this).closest('div.line');
    if (container.hasClass('done')) {
        container.removeClass('done');
        $.post('/Today/<%= Model.NameSlug %>/Undone', { 'Id': container.attr('goalid') }, function (data) { });        
    } else {
        container.addClass('done');
        $.post('/Today/<%= Model.NameSlug %>/Done', { 'Id': container.attr('goalid') }, function (data) { });        
    }
});

$('img.delete').live('click', function () {
    var goal = $(this).closest('div.line');
    $.post('/Today/<%= Model.NameSlug %>/RemoveGoal', { 'Id': goal.attr('goalid') }, function (data) { });
    goal.remove();
});

$('.goal_form').live('submit', function (event) {
    var textbox = $('.goal', $(this));
    var goal = textbox.val();
    $.post('/Today/<%= Model.NameSlug %>/AddGoal', $(this).serialize(), function (data) { });
    var newForm = textbox.closest('form').clone();
    $('input.goal', newForm).val('');
    textbox.closest('div.line').html(goal + '<img src="<%= Url.Content("~/Content/Images/delete.png") %>" alt="Delete" class="delete" /><input type="checkbox" class="toggle" />').addClass('full');
    $($('div.line:not(div.full)')[0]).html(newForm);
    $('div.lines').append('<div class="line"></div>');
    $($('.goal')[0]).focus();
    event.preventDefault();
});

</script>

</asp:Content>
