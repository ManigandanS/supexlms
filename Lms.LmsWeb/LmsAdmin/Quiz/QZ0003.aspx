﻿<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="QZ0003.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Quiz.QZ0003" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Quiz Details</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">


                    <div class="row">
                        <div class="col-xs-12">

                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                            </div>


                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="NameTextBox">Name</asp:Label>
                                <asp:TextBox ID="NameTextBox" runat="server" PlaceHolder="Name" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" Display="Dynamic" ErrorMessage="Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="DescriptionTextBox">Description</asp:Label>
                                <asp:TextBox ID="DescriptionTextBox" runat="server" PlaceHolder="Description" CssClass="form-control" AutoCompleteType="None" TextMode="MultiLine" Rows="12"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="DescriptionTextBox" Display="Dynamic" ErrorMessage="Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="PassPercentTextBox">Pass Percent</asp:Label>
                                <asp:TextBox ID="PassPercentTextBox" runat="server" PlaceHolder="Pass Percent" CssClass="form-control" AutoCompleteType="None" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="PassPercentTextBox" Display="Dynamic" ErrorMessage="Required" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>


                            <asp:Button ID="EditBtn" runat="server" Text="Save Changes" CssClass="btn btn-default" OnClick="EditBtn_Click" />
                            <asp:Button ID="DelBtn" runat="server" OnClick="DelBtn_Click" Text="Delete" CssClass="btn btn-danger" OnClientClick="return confirmDelete();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>


<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    

    <script>
        function confirmPublish() {
            return confirm('You can not modify question and answer after you publish. Are you sure to publish this quiz?');
        }
    </script>
</asp:Content>