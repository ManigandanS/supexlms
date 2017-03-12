<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CM0301.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Company.CM0301" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Announcements</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a href="CM0302" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Title</th>
                                            <th>Start Date</th>
                                            <th>End Date</th>
                                            <th>Modified Date</th>
                                            <th>Modified By</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="NotificationRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Title") %></td>
                                                    <td><%# Eval("StartDate") %></td>
                                                    <td><%# Eval("EndDate") %></td>
                                                    <td><%# Eval("UpdatedTs") %></td>
                                                    <td><%# Eval("User.DecryptedFullName") %></td>
                                                    <td style="text-align: right;">
                                                        <asp:HyperLink ID="EditLnk" runat="server" CssClass="btn btn-default btn-sm" Text="Details" NavigateUrl='<%# "CM0303?id=" + Eval("Id") %>' />
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
