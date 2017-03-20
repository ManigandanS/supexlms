<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AC0005.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Account.AC0005" %>
<%@ Register Src="~/UserControls/CommonSearch.ascx" TagName="SearchControl" TagPrefix="lms" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Add Manager</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <lms:SearchControl ID="SearchControl1" runat="server"></lms:SearchControl>

                    <div class="row">
                        <div class="col-xs-12">

                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>First Name</th>
                                            <th>Last Name</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("DecryptedFirstName") %></td>
                                                    <td><%# Eval("DecryptedLastName") %></td>
                                                    <td style="text-align: right;">
                                                        <asp:Button ID="AddBtn" runat="server" Text="Add" CssClass="btn btn-primary btn-sm" OnCommand="AddBtn_Command" CommandArgument='<%# Eval("Id") %>' />
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
