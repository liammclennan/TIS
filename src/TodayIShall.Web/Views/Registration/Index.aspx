<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Home.Master" Inherits="System.Web.Mvc.ViewPage<TodayIShall.Web.Models.NewAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Today I Shall - Simple daily planning tool
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="content">
    <h3>Register</h3>
    <% Html.BeginForm("Register", "Registration", FormMethod.Post, new {id="registration_form"}); %>
        <p><label for="FirstName">First Name:</label><br />
        <%= Html.TextBoxFor(m => m.FirstName) %><%= Html.ValidationMessageFor(m => m.FirstName) %></p>
        <p><label for="LastName">Last Name:</label><br />
        <%= Html.TextBoxFor(m => m.LastName)%><%= Html.ValidationMessageFor(m => m.LastName) %></p>
        <p><label for="Email">Email:</label><br />
        <%= Html.TextBoxFor(m => m.Email)%><%= Html.ValidationMessageFor(m => m.Email) %></p>
        <p><label for="Email">Time Zone:</label><br />
        <%= Html.DropDownListFor(m => m.TimeZoneInfoId, Model.TimeZones())%><%= Html.ValidationMessageFor(m => m.TimeZoneInfoId)%></p>
        <p><label for="Password">Password:</label><br />
        <%= Html.PasswordFor(m => m.Password)%><%= Html.ValidationMessageFor(m => m.Password) %></p>
        <p><label for="PasswordRepeat">Confirm Password:</label><br />
        <input type="password" name="PasswordRepeat" id="PasswordRepeat" /><span id="password_repeat_error" class='field-validation-error hidden_error'>'Password' and 'Confirm Password' must match.</span></p>
        <input type="submit" value="Continue" />
    <% Html.EndForm(); %>
</div>

<script type="text/javascript">

    $($('input')[0]).focus();

    $('form#registration_form').submit(function (e) {
        var passwordsMatch = $('input#Password').val() === $('input#PasswordRepeat').val();
        if (!passwordsMatch) {
            $('span#password_repeat_error').removeClass('hidden_error');
        }
        return passwordsMatch;
    });

</script>

</asp:Content>
