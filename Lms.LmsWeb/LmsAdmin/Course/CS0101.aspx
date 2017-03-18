<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CS0101.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Course.CS0101" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Curriculum</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <!-- section & lesson management -->
                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <asp:HyperLink ID="CreateLink" runat="server" Text="Create New" CssClass="btn btn-primary btn-sm"></asp:HyperLink>
                            </div>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Order</th>
                                        <th>Type</th>
                                        <th>Name</th>
                                        <th>Description</th>
                                        <th>Timestamp</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("Order") %></td>
                                                <td><%# Eval("LessonType") %></td>
                                                <td><%# Eval("Name") %></td>
                                                <td><%# Eval("Description") %></td>
                                                <td><%# Eval("UpdatedTs") %></td>
                                                <td style="text-align: right;">
                                                    <asp:HyperLink ID="EditLink" runat="server" Text="Edit" CssClass="btn btn-default btn-sm" NavigateUrl='<%# "CS0103?csid=" + Eval("CourseId") + "&lsid=" + Eval("Id") %>'></asp:HyperLink>
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


</asp:Content>
