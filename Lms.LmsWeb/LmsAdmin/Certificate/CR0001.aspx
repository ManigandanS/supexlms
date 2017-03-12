<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CR0001.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Certificate.CR0001" %>
<%@ Register Src="~/UserControls/CommonSearch.ascx" TagName="SearchControl" TagPrefix="lms" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Certificate</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <lms:SearchControl ID="SearchControl1" runat="server"></lms:SearchControl>

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a href="CR0002" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Description</th>
                                            <th>Expires</th>
                                            <th>Timestamp</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Name") %></td>
                                                    <td><%# Eval("Description") %></td>
                                                    <td><%# (int)Eval("ExpiryType") == 0 ? "No Expriy" : Eval("ExpiryMonth") + " Months" %></td>
                                                    <td><%# Eval("UpdatedTs") %></td>
                                                    <td style="text-align: right;">
                                                        <a href='CR0003?id=<%# Eval("Id") %>' class="btn btn-default btn-sm">Details</a>
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
</asp:Content>
