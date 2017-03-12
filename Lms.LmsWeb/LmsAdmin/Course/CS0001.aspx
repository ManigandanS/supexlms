<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CS0001.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Course.CS0001" %>

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
                                <asp:TextBox ID="CourseNameText" runat="server" CssClass="form-control" placeholder="Course Keyword"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="btn btn-default btn-block" OnClick="SearchBtn_Click" />
                            </div>
                        </div>
                    </section>

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <a href="CS0002" class="btn btn-primary btn-sm">Create New</a>
                            </div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Coursre Type</th>
                                            <th>Course Location</th>
                                            <th>Course Access</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="CourseRepeater" runat="server" OnItemDataBound="CourseRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Name") %></td>
                                                    <td><%# Eval("CourseType") %></td>
                                                    <td><%# Eval("CourseLocation") %></td>
                                                    <td><%# Eval("CourseAccessDisplay") %></td>
                                                    <td style="text-align: right;">
                                                        <asp:Button ID="PublishBtn" runat="server" OnCommand="PublishBtn_Command" CommandArgument='<%# Eval("Id") %>' Text="Publish" CssClass="btn btn-sm btn-primary" Visible='<%# !(bool)Eval("IsPublished") %>' OnClientClick="return confirmPublish();" />
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# "CS0003?csid=" + Eval("Id") %>' Text="Details" CssClass="btn btn-default btn-sm"></asp:HyperLink>
                                                        <asp:Button ID="CurriculumBtn" runat="server" Text="Curriculum" CssClass="btn btn-warning btn-sm"  Enabled='<%# (CourseTypeEnum)Eval("CourseType") == CourseTypeEnum.Intenral %>' CommandArgument='<%# Eval("Id") %>' OnCommand="CurriculumBtn_Command"/>
                                                        <a href="CS0201?csid=<%# Eval("Id") %>" class="btn btn-warning btn-sm">Session</a>
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


    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Courses</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                </div>
            </div>
        </div>
    </div>

    <script>
        function confirmPublish() {
            return confirm('You can not modify curriculum after you publish. Are you sure to publish this course?');
        }


    </script>
</asp:Content>
