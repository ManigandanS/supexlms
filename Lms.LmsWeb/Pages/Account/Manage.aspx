<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Lms.LmsWeb.Account.Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Profile</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label>
                                <p class="form-control-static">
                                    <asp:Literal ID="Email" runat="server"></asp:Literal></p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Name">Name</asp:Label>
                                <p class="form-control-static">
                                    <asp:Literal ID="Name" runat="server"></asp:Literal></p>
                            </div>

                            <div>
                                <a href="ChangePassword" class="btn btn-default">Change Password</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
