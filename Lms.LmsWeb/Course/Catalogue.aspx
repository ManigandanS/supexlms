<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogue.aspx.cs" Inherits="Lms.LmsWeb.Course.Catalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Course Catalogue</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <section id="search-course">
                        <div class="row">
                            <div class="col-sm-10">
                                <asp:TextBox ID="SearchText" runat="server" CssClass="form-control" placeholder="SEARCH"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" CssClass="btn btn-default btn-block" />
                            </div>
                        </div>
                    </section>

                    <div class="row">
                        <asp:Repeater ID="CourseRepeater" runat="server">
                            <ItemTemplate>
                                <div class="col-md-3 col-sm-6">
                                    <div class="course-wrapper">
                                        <div>
                                            <img src="../images/course-image.jpg" class="img-responsive" />
                                        </div>
                                        <div class="caption">
                                            <div class="course-title"><a href="Details?id=<%# Eval("Id") %>"><%# Eval("Name") %></a></div>
                                            <div class="course-desc"><%# Eval("Description") %></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
