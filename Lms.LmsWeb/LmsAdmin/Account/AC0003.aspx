<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AC0003.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Account.AC0003" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Account Details</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="row">
                        <div class="col-xs-12">

                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                            </div>

                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="active"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">Profile</a></li>
                                <li role="presentation"><a href="#courseRecord" aria-controls="courseRecord" role="tab" data-toggle="tab">Course</a></li>
                                <li role="presentation"><a href="#certificateRecord" aria-controls="certificateRecord" role="tab" data-toggle="tab">Certificate</a></li>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane fade in active" id="profile">
                                    <div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label>
                                            <asp:TextBox ID="Email" runat="server" PlaceHolder="Email" CssClass="form-control" TextMode="Email" AutoCompleteType="None" Enabled="false"></asp:TextBox>
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
                                    </div>
                                    <div>
                                        <asp:Button ID="EditBtn" runat="server" Text="Save Change" OnClick="EditBtn_Click" CssClass="btn btn-default" />
                                        <asp:Button ID="DelBtn" runat="server" Text="Delete" OnClick="DelBtn_Click" CssClass="btn btn-danger" />
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="courseRecord">
                                    <div>
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>Course Name</th>
                                                    <th>Completed Date</th>
                                                    <th>Result</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="EnrollRepeater" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("Session.Course.Name") %></td>
                                                            <td><%# Eval("CompletedTs") %></td>
                                                            <td><%# Eval("Result") %></td>
                                                            <td style="text-align: right;">
                                                                <a href='AC0004?enid=<%# Eval("Id") %>' class="btn btn-default btn-sm">Details</a>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>

                                        </table>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="certificateRecord">
                                    <div>
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>Certificate Name</th>
                                                    <th>Issue Date</th>
                                                    <th>Expiry Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="CertificateRepeater" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("Certificate.Name") %></td>
                                                            <td><%# Eval("IssuedTs") %></td>
                                                            <td><%# Eval("ExpireTs") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>

                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
