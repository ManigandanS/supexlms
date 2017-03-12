<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ScormLesson.aspx.cs" Inherits="Lms.LmsWeb.Course.ScormLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/scorm-api.js" type="text/javascript"></script>

    <input type="hidden" name="enid" id="enid" value="<%= enrollmentId %>" />
    <input type="hidden" name="lsid" id="lsid" value="<%= lessonId %>" />


    <script>

        var scormWin = window.open("<%= scormUrl %>", "_blank", "width=1280px,height=800px");

        var popupTick = setInterval(function () {
            if (scormWin != null && scormWin.closed) {
                clearInterval(popupTick);
                window.location.href = "Curriculum?lsid=<%= lessonId %>&enid=<%= enrollmentId %>";
                }
            }, 500);

            //scormWin.focus();
    </script>

</asp:Content>
