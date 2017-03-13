<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuizLesson.aspx.cs" Inherits="Lms.LmsWeb.Course.QuizLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <input type="hidden" name="enid" id="enid" value="<%= enrollmentId %>" />
    <input type="hidden" name="lsid" id="lsid" value="<%= lessonId %>" />

    <a name="questionTop"></a>


    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Quiz</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">


                    <div class="row">
                        <div class="col-sm-9">
                            <asp:Repeater ID="QuestionRepeater" runat="server" OnItemDataBound="QuestionRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <div id="questionId-<%# Eval("Id") %>" style='display: <%# Container.ItemIndex == 0 ? "block;" : "none;" %>' class="questionDiv">
                                        <p style="font-size: 1.5em; font-weight: 300;"><%# Eval("Title") %></p>

                                        <asp:Panel ID="QuestionAnswer" runat="server"></asp:Panel>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>


                        <div class="col-sm-3">
                            <asp:Repeater ID="QuizNumRepeater" runat="server">
                                <ItemTemplate>
                                    <div style="width: 32px; height: 32px; background: #eee; border: 1px solid #ccc; float: left; text-align: center; margin: 4px; line-height: 32px; cursor: pointer;" onclick='showQuestion(<%# "\"" + Eval("Id") + "\"" %>);'>
                                        <%# Eval("Order") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <div>
                                <asp:Button ID="SubmitBtn" runat="server" Text="Submit" CssClass="btn btn-primary btn-block" OnClick="SubmitBtn_Click" disabled="disabled" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <script>
        var questionNum = <%= controls.Count %>;
        var questionClicked = [];
        var questionAnswered = [];

        function showQuestion(questionId) {
            $(".questionDiv").css("display", "none");
            $("#questionId-" + questionId).css("display", "block");

            $('html,body').animate({ scrollTop: 0 }, 'slow');

           
            if (questionClicked.indexOf(questionId) == -1) {
                questionClicked.push(questionId);
            }

            
        }

        function markAnswered(questionId) {
            if (questionAnswered.indexOf(questionId) == -1) {
                questionAnswered.push(questionId);
            }

            if (questionNum == questionAnswered.length)
                $("#<%= SubmitBtn.ClientID %>").removeAttr("disabled");
        }
    </script>
</asp:Content>
