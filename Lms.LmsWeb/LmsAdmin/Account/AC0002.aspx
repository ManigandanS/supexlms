<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AC0002.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Account.AC0002" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Account</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label>
                                <asp:TextBox ID="Email" runat="server" PlaceHolder="Email" CssClass="form-control" TextMode="Email" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="ConfirmEmail">Confirm Email</asp:Label>
                                <asp:TextBox ID="ConfirmEmail" runat="server" PlaceHolder="Confirm Email" CssClass="form-control" TextMode="Email" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmEmail" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:CompareValidator runat="server" ControlToValidate="Email" ControlToCompare="ConfirmEmail" Display="Dynamic" ErrorMessage="Email does not match" CssClass="text-danger"></asp:CompareValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TemporaryPassword">Password</asp:Label>
                                <asp:TextBox ID="TemporaryPassword" runat="server" PlaceHolder="Temporary Password" CssClass="form-control" TextMode="Password" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TemporaryPassword" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm Password</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" PlaceHolder="Confirm Password" CssClass="form-control" TextMode="Password" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:CompareValidator runat="server" ControlToValidate="TemporaryPassword" ControlToCompare="ConfirmPassword" Display="Dynamic" ErrorMessage="Password does not match" CssClass="text-danger"></asp:CompareValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="FirstName">First Name</asp:Label>
                                <asp:TextBox ID="FirstName" runat="server" PlaceHolder="First Name" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="LastName">Last Name</asp:Label>
                                <asp:TextBox ID="LastName" runat="server" PlaceHolder="Last Name" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="UserType">User Type</asp:Label>
                                <asp:RadioButtonList ID="UserType" runat="server" CssClass="radio" Style="margin-left: 0;">
                                    <asp:ListItem Selected="True" Text="Internal" Value="0"></asp:ListItem>
                                    <asp:ListItem Selected="False" Text="External" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Group">Group</asp:Label>
                                <asp:CheckBoxList ID="Group" runat="server" CssClass="checkbox"></asp:CheckBoxList>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Role">Role</asp:Label>
                                <asp:CheckBoxList ID="Role" runat="server" CssClass="checkbox"></asp:CheckBoxList>
                            </div>

                            <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
