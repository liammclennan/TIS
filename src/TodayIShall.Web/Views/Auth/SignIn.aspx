<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sign In
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% Html.RenderPartial("_Menu"); %>

<%= Html.ValidationSummary() %>

<div class="content">
<% Html.BeginForm();%>
    <p>Password: <input type="password" class="signin" name="Password" id="Password" /></p>
    <p><input type="submit" value="Sign In" /></p>
    <%= Html.Hidden("ReturnUrl", Request["ReturnUrl"]) %>
<% Html.EndForm(); %>  
</div>

<script type="text/javascript">
    $('input#Password').focus();
</script>

</asp:Content>
