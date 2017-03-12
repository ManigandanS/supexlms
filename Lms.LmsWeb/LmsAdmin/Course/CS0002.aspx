<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CS0002.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Course.CS0002" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Course</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                        </div>
                        <div class="col-xs-12">
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
                                <asp:Label runat="server" AssociatedControlID="CourseTypeRadioButtonList">Course Type</asp:Label>
                                <asp:RadioButtonList ID="CourseTypeRadioButtonList" runat="server" CssClass="radio">
                                    <asp:ListItem Text="Internal Course" Value="0" Selected="True" />
                                    <asp:ListItem Text="External Course" Value="1" />
                                </asp:RadioButtonList>
                            </div>


                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CourseLocationRadioButtonList">Course Location</asp:Label>
                                <asp:RadioButtonList ID="CourseLocationRadioButtonList" runat="server" CssClass="radio">
                                    <asp:ListItem Text="Online" Value="0" Selected="True" />
                                    <asp:ListItem Text="Offline" Value="1" />
                                    <asp:ListItem Text="Both" Value="2" />
                                </asp:RadioButtonList>
                            </div>


                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CourseAccessDropDownList">Course Access</asp:Label>
                                <asp:DropDownList ID="CourseAccessDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Internal Users Only" Value="0" Selected="True" />
                                    <asp:ListItem Text="External Users Only" Value="1" />
                                    <asp:ListItem Text="Both Users" Value="2" />
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CertificateDropDownList">Certificate</asp:Label>
                                <asp:DropDownList ID="CertificateDropDownList" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>





    <script>

        $(document).ready(function () {
            $('#<%= CourseTypeRadioButtonList.ClientID %> input').change(function () {
                // The one that fires the event is always the
                // checked one; you don't need to test for this


                $("#<%= CourseAccessDropDownList.ClientID %>").empty();

                if ($(this).val() == 0) {
                    $("#<%= CourseAccessDropDownList.ClientID %>").append("<option value='0' selected='selected'>Internal Users Only</option>");
                    $("#<%= CourseAccessDropDownList.ClientID %>").append("<option value='1'>External Users Only</option>");
                    $("#<%= CourseAccessDropDownList.ClientID %>").append("<option value='2'>Both Users</option>");
                } else {
                    $("#<%= CourseAccessDropDownList.ClientID %>").append("<option value='0' selected='selected'>Internal Users Only</option>");
                }
            });
        });
    </script>



</asp:Content>
