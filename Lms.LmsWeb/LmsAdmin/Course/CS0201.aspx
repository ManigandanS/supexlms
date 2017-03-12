<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CS0201.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Course.CS0201" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Sessions</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <!-- session management -->
                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a class="btn btn-primary btn-sm" href="CS0202?csid=<%= courseId %>">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Session Start Date</th>
                                            <th>Session End Date</th>
                                            <th>Enroll Start Date</th>
                                            <th>Enroll End Date</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Name") %></td>
                                                    <td><%# Eval("SessionStart") %></td>
                                                    <td><%# Eval("SessionEnd") %></td>
                                                    <td><%# Eval("EnrollStart") %></td>
                                                    <td><%# Eval("EnrollEnd") %></td>
                                                    <td style="text-align: right;">
                                                        <a href='CS0203?ssid=<%# Eval("Id") %>' class="btn btn-default btn-sm">Edit</a>
                                                        <asp:Button ID="DelBtn" runat="server" OnCommand="DelBtn_Command" CommandArgument='<%# Eval("Id") %>' Text="Delete" CssClass="btn btn-danger btn-sm" />
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
