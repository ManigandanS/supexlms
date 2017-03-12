<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSelectAnswer.ascx.cs" Inherits="Lms.LmsWeb.UserControls.SingleSelectAnswer" %>

<asp:RadioButtonList ID="UserAnswerRadio" runat="server" CssClass="radio"></asp:RadioButtonList>

<script>
    $('#<%=UserAnswerRadio.ClientID %>').change(function () {
        markAnswered('<%= questionId %>');
    });
</script>