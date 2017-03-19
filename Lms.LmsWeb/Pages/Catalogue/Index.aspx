<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Lms.LmsWeb.Catalogue.Index" %>
<%@ Register Src="~/UserControls/CourseSearch.ascx" TagName="SearchControl" TagPrefix="lms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Course Catalogue</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <lms:SearchControl ID="SearchControl1" runat="server"></lms:SearchControl>

                    <div class="row">
                        <asp:Repeater ID="CourseRepeater" runat="server">
                            <ItemTemplate>
                                <div class="col-md-3 col-sm-6">
                                    <div class="course-wrapper">
                                        <div>
                                            <img src="/images/course-image.jpg" class="img-responsive" />
                                        </div>
                                        <div class="caption">
                                            <div class="course-title"><a href="Details?csid=<%# Eval("Id") %>"><%# Eval("Name") %></a></div>
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
