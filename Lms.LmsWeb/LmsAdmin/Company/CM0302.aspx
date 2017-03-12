<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CM0302.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Company.CM0302" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Announcement</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">


                    <link rel="stylesheet" type="text/css" href="/Content/bootstrap-datepiacker3.min.css" />

                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TitleText">Title</asp:Label>
                                <asp:TextBox ID="TitleText" runat="server" PlaceHolder="Title" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TitleText" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="StartDate">Start Date</asp:Label>
                                <div class="input-group date" data-provide="datepicker">
                                    <asp:TextBox ID="StartDate" runat="server" PlaceHolder="Start Date" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-th"></span>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDate" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="EndDate">End Date</asp:Label>
                                <div class="input-group date" data-provide="datepicker">
                                    <asp:TextBox ID="EndDate" runat="server" PlaceHolder="End Date" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-th"></span>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="EndDate" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Details">Details</asp:Label>
                                <asp:TextBox ID="Details" runat="server" CssClass="form-control" AutoCompleteType="None" TextMode="MultiLine" Rows="15"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Details" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-xs-12">
                            <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    
    <script src="/Scripts/bootstrap-datepicker.min.js"></script>
    <script src="/Scripts/tinymce/tinymce.min.js"></script>

    <script>
        $(document).ready(function () {
            tinymce.init({
                selector: 'textarea',
                encoding: "xml"
            });
        });
    </script>
</asp:Content>
