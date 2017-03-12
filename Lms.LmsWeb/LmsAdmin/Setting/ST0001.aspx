<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="ST0001.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Setting.ST0001" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Settings</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">                            
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Title</th>
                                            <th>Description</th>
                                            <th>Use</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="ConfigRepeater" runat="server" OnItemDataBound="ConfigRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Title") %></td>
                                                    <td><%# Eval("Description") %></td>
                                                    <td><asp:Literal ID="Use" runat="server"></asp:Literal></td>
                                                    <td style="text-align: right;">
                                                        <a href='<%# Eval("Code") %>' class="btn btn-default btn-sm">Details</a>
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