<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CS0102.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Course.CS0102" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Curriculum</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <section id="course-general">
                        <div class="row">
                            <div class="col-xs-12">
                                <div>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="CurriculumName">Curriculum Name</asp:Label>
                                    <asp:TextBox ID="CurriculumName" runat="server" PlaceHolder="Curriculum Name" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="CurriculumName" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="Description">Description</asp:Label>
                                    <asp:TextBox ID="Description" runat="server" PlaceHolder="Description" CssClass="form-control" AutoCompleteType="None" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                </div>

                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="CurriculumType">Curriculum Type</asp:Label>
                                        <asp:RadioButtonList ID="CurriculumType" runat="server" CssClass="radio" RepeatLayout="Flow" style="margin-top: 0;">
                                            <asp:ListItem Text="SCORM" Value="0" Selected="True" />
                                            <asp:ListItem Text="Quiz" Value="1" />
                                            <%--   
                    <asp:ListItem Text="Assignment" Value="2" />
                                            --%>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group" id="scorm-selector">
                                        <asp:Label runat="server" AssociatedControlID="ScormId">Select Content</asp:Label>
                                        <asp:HiddenField ID="ScormId" runat="server" />
                                        <div>
                                            <span class="text-danger" style="display: none;" id="scormIdValidator">Required</span>
                                        </div>
                                        <div>
                                            <input type="button" class="btn btn-default" value="Browse" data-toggle="modal" data-target="#scormModal" />
                                        </div>
                                    </div>

                                    <div class="form-group" id="quiz-selector" style="display: none;">
                                        <asp:Label runat="server" AssociatedControlID="QuizId">Select Quiz</asp:Label>
                                        <asp:HiddenField ID="QuizId" runat="server" />
                                        <div>
                                            <span class="text-danger" style="display: none;" id="quizIdValidator">Required</span>
                                        </div>
                                        <div>
                                            <input type="button" class="btn btn-default" value="Browse" data-toggle="modal" data-target="#quizModal" />
                                        </div>
                                    </div>

                                    <div class="form-group" id="assignment-selector" style="display: none;">
                                    </div>
                                </asp:Panel>


                                <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                            </div>

                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>





    <!-- Modal -->
    <div class="modal fade" id="scormModal" tabindex="-1" role="dialog" aria-labelledby="scormModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="scormModalLabel">Search SCORM</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>SCORM Name</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="ScormRepeater" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Name") %></td>
                                            <td style="text-align: right;">
                                                <input type="button" class="btn btn-primary btn-sm" value="Select" onclick='<%# "selectScorm(\"" + Eval("Id") + "\")" %>' /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>



    <!-- Modal -->
    <div class="modal fade" id="quizModal" tabindex="-1" role="dialog" aria-labelledby="quizModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="quizModalLabel">Search Quiz</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Quiz Name</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="QuizRepeater" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Title") %></td>
                                            <td style="text-align: right;">
                                                <input type="button" class="btn btn-primary btn-sm" value="Select" onclick='<%# "selectQuiz(\"" + Eval("Id") + "\")" %>' /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function selectScorm(scormId) {
            $("#<%= ScormId.ClientID %>").val(scormId);
            $('#scormModal').modal('toggle');
        }

        function selectQuiz(quizId) {
            $("#<%= QuizId.ClientID %>").val(quizId);
            $('#quizModal').modal('toggle');
        }

        $("form").on( "submit", function( event ) {
            //event.preventDefault();
            $("#scormIdValidator").css("display", "none");
            $("#quizIdValidator").css("display", "none"); 

            var lessonType = $('#<%= CurriculumType.ClientID %>').find(":checked").val();
            
            if (lessonType == 0 && $("#<%= ScormId.ClientID %>").val() == "") {                
                $("#scormIdValidator").css("display", "inline-block"); 
                return false;
            } else if (lessonType == 1 && $("#<%= QuizId.ClientID %>").val() == "") {
                $("#quizIdValidator").css("display", "inline-block"); 
                return false;
            } else if (lessonType == 2) {
                return false;
            }

            return true;
        });

        $(document).ready(function () {
            $('#<%= CurriculumType.ClientID %> input').change(function () {
                // The one that fires the event is always the
                // checked one; you don't need to test for this
                $("#scorm-selector").css("display", "none");
                $("#quiz-selector").css("display", "none");
                $("#assignment-selector").css("display", "none");

                if ($(this).val() == 0) {
                    $("#scorm-selector").css("display", "block");
                } else if ($(this).val() == 1) {
                    $("#quiz-selector").css("display", "block");
                } else {
                    $("#assignment-selector").css("display", "block");
                }
            });            
        });

    </script>



</asp:Content>
