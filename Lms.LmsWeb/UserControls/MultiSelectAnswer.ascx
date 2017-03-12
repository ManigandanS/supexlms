<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiSelectAnswer.ascx.cs" Inherits="Lms.LmsWeb.UserControls.MultiSelectAnswer" %>


<asp:CheckBoxList ID="UserAnswerCheck" runat="server" CssClass="checkbox"></asp:CheckBoxList>

<script>
    $('#<%=UserAnswerCheck.ClientID %>').change(function () {
        markAnswered('<%= questionId %>');
    });
</script>