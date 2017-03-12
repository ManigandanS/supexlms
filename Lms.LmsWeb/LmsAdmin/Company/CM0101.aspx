<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CM0101.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Company.CM0101" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Groups</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <!-- group management -->
                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a href="CM0102" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Description</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="GroupRepeater" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("Name") %></td>
                                                <td><%# Eval("Description") %></td>
                                                <td style="text-align: right;">
                                                    <asp:HyperLink ID="EditLnk" runat="server" Visible='<%# (bool)Eval("CanDeleted") %>' CssClass="btn btn-default btn-sm" Text="Details" NavigateUrl='<%# "CM0103?grid=" + Eval("Id") %>' />
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
