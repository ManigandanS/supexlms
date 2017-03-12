<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QZ0101.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Quiz.QZ0101" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Questions</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <asp:HyperLink ID="CreateLink" runat="server" Text="Create New" CssClass="btn btn bg-primary btn-sm"></asp:HyperLink>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Order</th>
                                            <th>Title</th>
                                            <th>Type</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="QuestionRepeater" runat="server" OnItemDataBound="QuizRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                    <td><%# Eval("Title") %></td>
                                                    <td><%# Eval("TypeDisplay") %></td>
                                                    <td style="text-align: right;">
                                                        <a href='QZ0103?qzid=<%# Eval("QuizId") %>&qsid=<%# Eval("Id") %>' class="btn btn-default btn-sm">Details</a>
                                                        <a href='QZ0104?qzid=<%# Eval("QuizId") %>&qsid=<%# Eval("Id") %>' class="btn btn-default btn-sm">Preview</a>
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
