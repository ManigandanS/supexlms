<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QZ0102.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Quiz.QZ0102" %>

<%@ Register Src="~/UserControls/MultiSelectAnswer.ascx" TagName="MultiSelectAnswer" TagPrefix="lms" %>
<%@ Register Src="~/UserControls/SingleSelectAnswer.ascx" TagName="SingleSelectAnswer" TagPrefix="lms" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Question</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">


                    <section>
                        <div class="col-xs-12">

                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                            </div>


                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TitleTextBox">Question Title</asp:Label>
                                <asp:TextBox ID="TitleTextBox" runat="server" PlaceHolder="Title" CssClass="form-control" AutoCompleteType="None" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TitleTextBox" Display="Dynamic" ErrorMessage="Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TypeRadio">Answer Type</asp:Label>
                                <asp:RadioButtonList ID="TypeRadio" runat="server" PlaceHolder="Name" CssClass="radio" ClientIDMode="Static">
                                    <asp:ListItem Text="Single Select" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Multiple Select" Value="1" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Keyword" Value="2" Selected="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="form-group" id="single-select-answer">
                                <label>Answer List</label>
                                <div id="single-select-list">
                                    <div class="input-group" id="single-select-list-1">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio1" runat="server" GroupName="SingleSelect" Checked="true" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="single-select-list-2" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio2" runat="server" GroupName="SingleSelect" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="single-select-list-3" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio3" runat="server" GroupName="SingleSelect" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="single-select-list-4" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio4" runat="server" GroupName="SingleSelect" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer4" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="single-select-list-5" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio5" runat="server" GroupName="SingleSelect" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer5" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="single-select-list-6" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio6" runat="server" GroupName="SingleSelect" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer6" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="single-select-list-7" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio7" runat="server" GroupName="SingleSelect" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer7" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="single-select-list-8" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:RadioButton ID="SingleSelectRadio8" runat="server" GroupName="SingleSelect" />
                                        </span>
                                        <asp:TextBox ID="SingleSelectAnswer8" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group" id="multiple-select-answer" style="display: none;">
                                <label>Answer List</label>
                                <div id="multiple-select-list">
                                    <div class="input-group" id="multiple-select-list-1">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck1" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="multiple-select-list-2" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck2" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="multiple-select-list-3" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck3" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="multiple-select-list-4" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck4" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer4" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="multiple-select-list-5" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck5" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer5" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="multiple-select-list-6" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck6" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer6" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="multiple-select-list-7" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck7" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer7" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-group" id="multiple-select-list-8" style="margin-top: 8px;">
                                        <span class="input-group-addon">
                                            <asp:CheckBox ID="MultiSelectCheck8" runat="server" />
                                        </span>
                                        <asp:TextBox ID="MultiSelectAnswer8" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group" id="keyword-answer" style="display: none;">
                                <asp:Label runat="server" AssociatedControlID="KeywordAnswer">Right Answer</asp:Label>
                                <asp:TextBox ID="KeywordAnswer" runat="server" PlaceHolder="Keyword Answer" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                            </div>


                            <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>





</asp:Content>


<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    


    <script>

        $(document).ready(function () {
            $('#TypeRadio input').change(function () {
                // The one that fires the event is always the
                // checked one; you don't need to test for this
                $("#keyword-answer").css("display", "none");
                $("#single-select-answer").css("display", "none");
                $("#multiple-select-answer").css("display", "none");

                if ($(this).val() == 0) {
                    $("#single-select-answer").css("display", "block");
                } else if ($(this).val() == 1) {
                    $("#multiple-select-answer").css("display", "block");
                } else if ($(this).val() == 2) {
                    $("#keyword-answer").css("display", "block");
                }
            });
        });

    </script>

</asp:Content>