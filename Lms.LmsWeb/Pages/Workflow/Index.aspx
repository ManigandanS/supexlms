<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Lms.LmsWeb.Pages.Workflow.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Workflow</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div>

                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation" class="active"><a href="#activeRecord" aria-controls="home" role="tab" data-toggle="tab">Active</a></li>
                                    <li role="presentation"><a href="#closedRecord" aria-controls="profile" role="tab" data-toggle="tab">Closed</a></li>
                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade in active" id="activeRecord">
                                        
                                        <div>
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>Subject</th>
                                                        <th>Status</th>
                                                        <th>Requestor</th>
                                                        <th>Request Date</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="ActiveRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("Subject") %></td>
                                                                <td><%# Eval("WorkflowProcessStatus") %></td>
                                                                <td><%# Eval("Requestor.DecryptedFullName") %></td>
                                                                <td><%# Eval("RequestTs") %></td>
                                                                <td style="text-align: right;">
                                                                    <a href='Review?wkid=<%# Eval("Id") %>' class="btn btn-primary btn-sm">Review</a>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>

                                            </table>
                                        </div>
                                    </div>

                                    <div role="tabpanel" class="tab-pane" id="closedRecord">
                                        
                                        <div>
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>Subject</th>
                                                        <th>Status</th>
                                                        <th>Requestor</th>
                                                        <th>Request Date</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="ClosedRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("Subject") %></td>
                                                                <td><%# Eval("WorkflowProcessStatus") %></td>
                                                                <td><%# Eval("Requestor") %></td>
                                                                <td><%# Eval("RequestTs") %></td>
                                                                <td style="text-align: right;">
                                                                    <a href='Review?wkid=<%# Eval("Id") %>' class="btn btn-primary btn-sm">Review</a>
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
            </div>
        </div>
    </div>


</asp:Content>

