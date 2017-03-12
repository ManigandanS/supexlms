<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AC0102.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Account.AC0102" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Role</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Name">Name</asp:Label>
                                <asp:TextBox ID="Name" runat="server" PlaceHolder="Name" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Description">Description</asp:Label>
                                <asp:TextBox ID="Description" runat="server" PlaceHolder="Description" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Description" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>


                            <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
