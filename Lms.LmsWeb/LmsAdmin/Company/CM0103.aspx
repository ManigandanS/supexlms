<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CM0103.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Company.CM0103" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Group Details</h2>
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


                            <asp:Button ID="EditBtn" runat="server" Text="Save Changes" CssClass="btn btn-default" OnClick="EditBtn_Click" />
                            <asp:Button ID="DelBtn" runat="server" Text="Delete" CssClass="btn btn-danger btn" OnClick="DelBtn_Click" Visible='<%# (bool)Eval("CanDeleted") %>' />                            
                                                    
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
