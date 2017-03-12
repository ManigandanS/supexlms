<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AC0001.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Account.AC0001" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Accounts</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <section id="search-user">
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:TextBox ID="FirstNameText" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="LastNameText" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-2">
                                <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="btn btn-default btn-block" OnClick="SearchBtn_Click" />
                            </div>
                        </div>
                    </section>

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                            </div>



                            <div>
                                <a href="AC0002" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Type</th>
                                            <!--
                    <th>Organization</th>
                    -->
                                            <th>Email</th>
                                            <th>First Name</th>
                                            <th>Last Name</th>
                                            <th>Acquisition</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("UserType") %></td>
                                                    <!--
                            <td></td>
                            -->
                                                    <td><%# Eval("Email") %></td>
                                                    <td><%# Eval("DecryptedFirstName") %></td>
                                                    <td><%# Eval("DecryptedLastName") %></td>
                                                    <td><%# Eval("Acquisition").ToString() %></td>
                                                    <td><%# Eval("Status") %></td>
                                                    <td style="text-align: right;">
                                                        <a href='AC0003?id=<%# Eval("Id") %>' class="btn btn-default btn-sm">Details</a>
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
