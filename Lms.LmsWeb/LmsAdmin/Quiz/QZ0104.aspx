<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QZ0104.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Quiz.QZ0104" %>

<%@ Register Src="~/UserControls/MultiSelectAnswer.ascx" TagName="MultiSelectAnswer" TagPrefix="lms" %>
<%@ Register Src="~/UserControls/SingleSelectAnswer.ascx" TagName="SingleSelectAnswer" TagPrefix="lms" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Preview Question</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <section>
                        <div class="col-xs-12">
                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                            </div>

                            <p style="font-size: 1.5em; font-weight: 300;">
                                <asp:Literal ID="QuestionTitle" runat="server"></asp:Literal></p>
                            <asp:Panel ID="AnswerPanel" runat="server"></asp:Panel>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
