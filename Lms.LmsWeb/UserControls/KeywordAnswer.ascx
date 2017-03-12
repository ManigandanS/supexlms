<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KeywordAnswer.ascx.cs" Inherits="Lms.LmsWeb.UserControls.KeywordAnswer" %>


<asp:TextBox ID="UserAnswerText" runat="server" CssClass="form-control"></asp:TextBox>

<script>
    $("#<%= UserAnswerText.ClientID %>").keyup(function () {
        markAnswered('<%= questionId %>');
    });
</script>