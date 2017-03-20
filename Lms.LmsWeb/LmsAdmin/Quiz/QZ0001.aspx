<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QZ0001.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Quiz.QZ0001" %>
<%@ Register Src="~/UserControls/CommonSearch.ascx" TagName="SearchControl" TagPrefix="lms" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Quizzes</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <lms:SearchControl ID="SearchControl1" runat="server"></lms:SearchControl>

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a href="QZ0002" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Title</th>
                                            <th>Description</th>
                                            <th>Pass Percent</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="QuizRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Title") %></td>
                                                    <td><%# Eval("Description") %></td>
                                                    <td><%# Eval("PassPercent") %></td>
                                                    <td style="text-align: right;">
                                                        <a href="<%# "QZ0003?id=" + Eval("Id") %>" class="btn btn-default btn-sm">Details</a>

                                                        <a href='QZ0101?qzid=<%# Eval("Id") %>' class="btn btn-default btn-sm">Question</a>
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
        </div>
    </div>

</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    

    <script>
        function confirmPublish() {
            return confirm('You can not modify question and answer after you publish. Are you sure to publish this quiz?');
        }


    </script>
</asp:Content>