<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="Lms.LmsWeb.Pages.Catalogue.Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Request Approval</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="ApproverName">Approver Name</asp:Label>
                                <p><asp:Literal ID="ApproverName" runat="server"></asp:Literal></p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CourseName">Course Name</asp:Label>
                                <p><asp:Literal ID="CourseName" runat="server"></asp:Literal></p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="SessionName">Session Name</asp:Label>
                                <p><asp:Literal ID="SessionName" runat="server"></asp:Literal></p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="SessionDate">Session Date</asp:Label>
                                <p><asp:Literal ID="SessionDate" runat="server"></asp:Literal></p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Cost">Cost</asp:Label>
                                <p><asp:Literal ID="Cost" runat="server"></asp:Literal></p>
                            </div>


                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Comment">Comment</asp:Label>
                                <asp:TextBox ID="Comment" runat="server" CssClass="form-control" TextMode="MultiLine" Height="80"></asp:TextBox>
                            </div>

                            <div>
                                <asp:Literal ID="Message" runat="server"></asp:Literal>
                            </div>
                            <asp:Button ID="RequestBtn" runat="server" Text="Request Approval" CssClass="btn btn-default" OnClick="RequestBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
