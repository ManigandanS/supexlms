<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="ST0002.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Setting.ST0002" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Office 365 Single Sign On</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TitleTextBox">Name</asp:Label>
                                <div>
                                    <asp:Literal ID="TitleTextBox" runat="server" Text="Office 365 Sign In"></asp:Literal>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="EndPointTextBox">End Point</asp:Label>
                                <asp:TextBox ID="EndPointTextBox" runat="server" PlaceHolder="End Point" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="EndPointTextBox" Display="Dynamic" ErrorMessage="Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="AppIDTextBox">App ID</asp:Label>
                                <asp:TextBox ID="AppIDTextBox" runat="server" PlaceHolder="App ID" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="AppIDTextBox" Display="Dynamic" ErrorMessage="Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div>
                                <asp:Button ID="SaveBtn" runat="server" Text="Save Changes" CssClass="btn btn-default" OnClick="SaveBtn_Click" />
                                <asp:Button ID="DelBtn" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="DelBtn_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
