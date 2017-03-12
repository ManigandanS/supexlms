<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AC0101.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Account.AC0101" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Roles</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <!-- group management -->
                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a href="AC0102" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Description</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RoleRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Name") %></td>
                                                    <td><%# Eval("Description") %></td>
                                                    <td style="text-align: right;">
                                                        <a href='AC0103?rlid=<%# Eval("Id") %>' class="btn btn-default btn-sm">Edit</a>
                                                        <asp:Button ID="DelBtn" runat="server" OnCommand="DelBtn_Command" CommandArgument='<%# Eval("Id") %>' Text="Delete" CssClass="btn btn-danger btn-sm" /></td>
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
