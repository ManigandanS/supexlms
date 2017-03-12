<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Trial.aspx.cs" Inherits="Lms.WebApp.Trial" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 20px;" class="col-md-12">

        <div>
            <asp:CustomValidator ID="CustomValidator1" runat="server" />
        </div>

        <fieldset style="margin-bottom: 20px;">

            <div class="editor-label">
                <label>Company Name</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="CompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CompanyName"></asp:RequiredFieldValidator>
            </div>

            <div class="editor-label">
                <label>Sub Domain</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="SubDomain" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="SubDomain"></asp:RequiredFieldValidator>
            </div>

            <div class="editor-label">
                <label>First Name</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="FirstName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FirstName"></asp:RequiredFieldValidator>
            </div>

            <div class="editor-label">
                <label>Last Name</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="LastName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LastName"></asp:RequiredFieldValidator>
            </div>


            <div class="editor-label">
                <label>Phone Number</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="PhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="PhoneNumber"></asp:RequiredFieldValidator>
            </div>

            <div class="editor-label">
                <label>Email</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Email"></asp:RequiredFieldValidator>
            </div>

            <div class="editor-label">
                <label>Password</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Password"></asp:RequiredFieldValidator>
            </div>

            <div class="editor-label">
                <label>Password Confirm</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="PasswordConfirm" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="PasswordConfirm"></asp:RequiredFieldValidator>
            </div>

        </fieldset>

        <asp:Button ID="RequestButton" runat="server" OnClick="RequestButton_Click" Text="Request" CssClass="btn btn-primary" />
    </div>
</asp:Content>
