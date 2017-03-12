<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CN0002.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Content.CN0002" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Content</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Name">Name</asp:Label>
                                <asp:TextBox ID="Name" runat="server" CssClass="form-control" placeholder="Name" />
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Description">Description</asp:Label>
                                <asp:TextBox ID="Description" runat="server" CssClass="form-control" placeholder="Description" />
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Type">Type</asp:Label>

                                <asp:RadioButtonList ID="Type" runat="server" CssClass="radio">
                                    <asp:ListItem Text="SCORM" Selected="True" Value="SCORM"></asp:ListItem>
                                    <asp:ListItem Text="Power Point" Selected="False" Value="PPT" Enabled="false"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>


                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="FileUpload1">File</asp:Label>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>

                            <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
