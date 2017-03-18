<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lms.LmsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4 col-md-push-8">
            <div class="x_panel">
                <div class="x_title">
                    <h2><i class="fa fa-bullhorn" aria-hidden="true"></i>&nbsp;Announcement</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <asp:Repeater ID="ActiveNotifications" runat="server">
                        <ItemTemplate>
                            <div onclick="toggleDetails('<%# "notification-" + Eval("Id") %>');">
                                <h5><strong><%# Eval("Title") %></strong></h5>
                            </div>
                            <div onclick="toggleDetails('<%# "notification-" + Eval("Id") %>');"><%# HttpUtility.HtmlDecode((string)Eval("Details")) %></div>
                            <div id="notification-<%# Eval("Id") %>" style="display: none;">
                                <span><%# Eval("StartDate") %></span>
                                <span><%# Eval("EndDate") %></span>
                                <span><%# Eval("Details") %></span>
                            </div>
                            <hr />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="col-md-8 col-md-pull-4">
            <asp:Panel ID="Panel1" runat="server" Cssclass="x_panel">
                <div class="x_title">
                    <h2><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;Active Courses</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <asp:Repeater ID="ActiveCourses" runat="server">
                        <ItemTemplate>
                            <a href='/Pages/Course/Curriculum?enid=<%# Eval("Id") %>'>
                                <div class="col-md-4 col-sm-6">
                                    <div>
                                        <img src="../images/course-image.jpg" class="img-responsive"></div>
                                    <div>
                                        <h5><strong><%# Eval("Session.Course.Name") %></strong></h5>
                                    </div>
                                    <div><%# Eval("Session.Name") %></div>
                                    <div><%# Eval("Session.SessionStart") %></div>
                                    <div><%# Eval("Session.SessionEnd") %></div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </asp:Panel>


            <asp:Panel ID="Panel2" runat="server" Cssclass="x_panel">
                <div class="x_title">
                    <h2><i class="fa fa-asterisk" aria-hidden="true"></i>&nbsp;Mandatory Courses</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <a href='/Pages/Course/Curriculum?enid=<%# Eval("Id") %>'>
                                <div class="col-md-4 col-sm-6">
                                    <div>
                                        <img src="../images/course-image.jpg" class="img-responsive"></div>
                                    <div>
                                        <h5><strong><%# Eval("Session.Course.Name") %></strong></h5>
                                    </div>
                                    <div><%# Eval("Session.Name") %></div>
                                    <div><%# Eval("Session.SessionStart") %></div>
                                    <div><%# Eval("Session.SessionEnd") %></div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </asp:Panel>

            <asp:Panel ID="Panel3" runat="server" CssClass="x_panel">
                <div class="x_title">
                    <h2><i class="fa fa-plus-square-o" aria-hidden="true"></i>&nbsp;New Courses</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <asp:Repeater ID="NewCourses" runat="server">
                        <ItemTemplate>
                            <a href='/Pages/Catalogue/Details?csid=<%# Eval("CourseId") %>'>
                                <div class="col-md-4 col-sm-6">
                                    <div>
                                        <img src="../images/course-image.jpg" class="img-responsive"></div>
                                    <div>
                                        <h5><strong><%# Eval("Course.Name") %></strong></h5>
                                    </div>
                                    <div><%# Eval("Name") %></div>
                                    <div><%# Eval("SessionStart") %></div>
                                    <div><%# Eval("SessionEnd") %></div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>                    
                </div>
            </asp:Panel>
        </div>
    </div>







    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel"></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            Start Date: <span id="myModalStart"></span>
                        </div>
                        <div class="col-md-6">
                            End Date: <span id="myModalEnd"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="myModalContent"></div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function toggleDetails(id) {

            var title = $("#" + id).prev().prev().prev().text();
            var startDate = $("#" + id + " span:eq(0)").text();
            var endDate = $("#" + id + " span:eq(1)").text();
            var content = $("#" + id + " span:eq(2)").text();
            $("#myModalLabel").text(title);
            $("#myModalContent").html(content);
            $("#myModalStart").html(startDate);
            $("#myModalEnd").html(endDate);
            $("#myModal").modal('show');
        }
    </script>

</asp:Content>
