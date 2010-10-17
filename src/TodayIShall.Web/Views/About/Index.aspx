<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	About
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% Html.RenderPartial("_Menu"); %>

<div class="content"><h3>About</h3>
    <p>
        This application is a simple planning tool for listing the day's goals and tracking your progress against them.
    </p>
    <p> 
        At the beginning of each day, open your daily plan and list your goals for the day. As you finish each goal mark it as complete. At the end of the day review your progress. 
    </p>
    <p>Potential benefits include:</p>
    <ul><li>
    Increased focus
    </li>
    <li>
    Motivation to get more done
    </li>
    </ul>
    <p></p>
    <p></p>
</div>

</asp:Content>
