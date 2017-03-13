<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transcript.aspx.cs" Inherits="Lms.LmsWeb.Account.Transcript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Transcript</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div>

                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation" class="active"><a href="#gradeRecord" aria-controls="home" role="tab" data-toggle="tab">Grade</a></li>
                                    <li role="presentation"><a href="#certificateRecord" aria-controls="profile" role="tab" data-toggle="tab">Certificate</a></li>
                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade in active" id="gradeRecord">
                                        
                                        <div>
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>Course Name</th>
                                                        <th>Completed Date</th>
                                                        <th>Result</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="GradeRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("Session.Course.Name") %></td>
                                                                <td><%# Eval("CompletedTs") %></td>
                                                                <td><%# Eval("Result") %></td>
                                                                <td style="text-align: right;">
                                                                    <a href='/Course/Curriculum?enid=<%# Eval("Id") %>' class="btn btn-primary btn-sm">Open</a>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>

                                            </table>
                                        </div>
                                    </div>

                                    <div role="tabpanel" class="tab-pane" id="certificateRecord">
                                        
                                        <div>
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>Certificate Name</th>
                                                        <th>Issue Date</th>
                                                        <th>Expiry Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="CertificateRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("Certificate.Name") %></td>
                                                                <td><%# Eval("IssuedTs") %></td>
                                                                <td><%# Eval("ExpireTs") %></td>
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
