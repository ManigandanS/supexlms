<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AC0004.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Account.AC0004" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Curriculum Details</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="row">
                        <div class="col-xs-12">

                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Order</th>
                                            <th>Type</th>
                                            <th>Name</th>
                                            <th>Description</th>
                                            <th>Status</th>
                                            <th>Score</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="LessonRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 1%;"><%# Eval("Lesson.Order") %></td>
                                                    <td><%# Eval("Lesson.LessonType") %></td>
                                                    <td><%# Eval("Lesson.Name") %></td>
                                                    <td><%# Eval("Lesson.Description") %></td>
                                                    <td><%# Eval("DataResultDisplay") %></td>
                                                    <td><%# Eval("Score") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        function launchScorm(enrollmentId, lessonId) {
            //alert(enrollmentId + " | " + lessonId);
            window.location.href = "ScormLesson?lsid=" + lessonId + "&enid=" + enrollmentId;
        }

        function takeQuiz(enrollmentId, lessonId) {
            //alert(enrollmentId + " | " + lessonId);
            window.location.href = "QuizLesson?lsid=" + lessonId + "&enid=" + enrollmentId;
        }
    </script>
</asp:Content>
