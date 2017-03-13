<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Lms.LmsWeb.Course.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Course</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div><h2><asp:Literal ID="CourseName" runat="server"></asp:Literal></h2></div>
                    <div><asp:Literal ID="CourseDesc" runat="server"></asp:Literal></div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Curriculum</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Order</th>
                                    <th>Type</th>
                                    <th>Name</th>
                                    <th>Description</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="LessonRepeater" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Order") %></td>
                                            <td><%# Eval("LessonType") %></td>
                                            <td><%# Eval("Name") %></td>
                                            <td><%# Eval("Description") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>


            <div class="x_panel">
                <div class="x_title">
                    <h2>Session</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Session Date</th>
                                    <th>Enroll Date</th>
                                    <th>Cost</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="SessionRepeater" runat="server" OnItemDataBound="SessionRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Name") %></td>
                                            <td><%# Eval("SessionStart") %> ~ <%# Eval("SessionEnd") %></td>
                                            <td><%# Eval("EnrollStart") %> ~ <%# Eval("EnrollEnd") %></td>
                                            <td><%# Eval("Cost") %></td>
                                            <td style="text-align: right;">
                                                <asp:HyperLink ID="EnrollLink" runat="server" Width="80">
                                                    <asp:Literal ID="EnrollStatus" runat="server"></asp:Literal>
                                                </asp:HyperLink>
                                                <asp:Button ID="EnrollBtn" runat="server" Width="80" Text="Enroll" CssClass="btn btn-primary btn-sm" OnCommand="EnrollBtn_Command" CommandArgument='<%# Eval("Id") %>' />
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
</asp:Content>
