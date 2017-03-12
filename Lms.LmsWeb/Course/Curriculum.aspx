<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Curriculum.aspx.cs" Inherits="Lms.LmsWeb.Course.Curriculum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-6">
            <div class="x_panel">                
                <div class="x_title">
                    <h2>Course</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div><h2><label>Course Name:</label><asp:Literal ID="CourseName" runat="server"></asp:Literal></h2></div>
                    <div><label>Course Description:</label><asp:Literal ID="CourseDesc" runat="server"></asp:Literal></div>
                    <div><label>Session Name:</label><asp:Literal ID="SessionName" runat="server"></asp:Literal></div>
                    <div><label>Session Description:</label><asp:Literal ID="SessionDesc" runat="server"></asp:Literal></div>                    
                </div>
            </div>
        </div>

        <div class="col-sm-6">
            <div class="x_panel">                
                <div class="x_title">
                    <h2>Status</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div><label>Status:</label><asp:Literal ID="EnrollStatus" runat="server"></asp:Literal></div>
                    <div><label>Result:</label><asp:Literal ID="EnrollResult" runat="server"></asp:Literal></div>
                    <div>
                        <asp:Button ID="WithdrawBtn" runat="server" OnClick="WithdrawBtn_Click" CssClass="btn btn-danger" Text="Withdraw" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Curriculum</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

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
                                    <th></th>
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
                                            <td style="text-align: right;">
                                                <input type="button" value="Launch" style="width: 70px;" class="btn btn-default btn-sm" runat="server" visible='<%# (LessonTypeEnum)Eval("Lesson.LessonType") == LessonTypeEnum.Content %>' onclick='<%# "launchScorm(\"" + enrollmentId + "\", \"" + Eval("Lesson.Id") + "\")" %>' />
                                                <input type="button" value="Take" style="width: 70px;" class="btn btn-default btn-sm" runat="server" visible='<%# (LessonTypeEnum)Eval("Lesson.LessonType") == LessonTypeEnum.Quiz %>' onclick='<%# "takeQuiz(\"" + enrollmentId + "\", \"" + Eval("Lesson.Id") + "\")" %>' />
                                            </td>
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
