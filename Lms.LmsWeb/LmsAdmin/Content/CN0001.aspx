<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CN0001.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Content.CN0001" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Courses</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <section id="search-user">
                        <div class="row">
                            <div class="col-sm-10">
                                <asp:TextBox ID="ScormNameText" runat="server" CssClass="form-control" placeholder="Content Keyword"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="btn btn-default btn-block" OnClick="SearchBtn_Click" />
                            </div>
                        </div>
                    </section>

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a href="CN0002" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Web Path</th>
                                            <th>Modified Date</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="ScormRepeater" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Name") %></td>
                                                    <td><%# Eval("WebPath") %></td>
                                                    <td><%# Eval("UpdatedTs") %></td>
                                                    <td style="text-align: right;">
                                                        <asp:Button ID="PublishBtn" runat="server" OnCommand="PublishBtn_Command" CommandArgument='<%# Eval("Id") %>' Text="Publish" CssClass="btn btn-sm btn-primary" Visible='<%# !(bool)Eval("IsPublished") %>' OnClientClick="return confirmPublish();" />
                                                        <a href="<%# "CN0003?id=" + Eval("Id") %>" class="btn btn-sm btn-default">Details</a>
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


    <script>
        function confirmPublish() {
            return confirm('You can not modify scorm after you publish. Are you sure to publish this scorm?');
        }

    </script>
</asp:Content>
