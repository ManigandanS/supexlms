<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CR0003.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Certificate.CR0003" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Certificate Details</h2>
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
                                <asp:TextBox ID="Description" runat="server" CssClass="form-control" placeholder="Description" TextMode="MultiLine" Rows="5" />
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="ExpiryType">Expiry Type</asp:Label>
                                <asp:RadioButtonList ID="ExpiryType" runat="server" CssClass="radio">
                                    <asp:ListItem Text="No Expiry" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Term" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <asp:Panel ID="ExpiryMonthPanel" runat="server" CssClass="form-group">
                                <asp:Label runat="server" AssociatedControlID="ExpiryMonth">Expiry Month</asp:Label>
                                <asp:TextBox ID="ExpiryMonth" runat="server" CssClass="form-control" placeholder="Expiry Month" />
                            </asp:Panel>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CourseRepeater">Attached Courses</asp:Label>
                                <table class="table">
                                    <asp:Repeater ID="CourseRepeater" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("Name") %></td>
                                                <td><%# Eval("Description") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>

                            <asp:Button ID="EditBtn" runat="server" Text="Save Changes" CssClass="btn btn-default" OnClick="EditBtn_Click" />
                            <asp:Button ID="DelBtn" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="DelBtn_Click" OnClientClick="return confirmDelete();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    
</asp:Content>


<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        function confirmDelete() {
            return confirm('You are about to delete the certificate. Are you sure to delete this certificate?');
        }


        $(document).ready(function () {

            if (<%= showExpiryPanel ? "true" : "false" %> == true) {
                $("#<%= ExpiryMonthPanel.ClientID %>").css("display", "block");
            } else {
                    $("#<%= ExpiryMonthPanel.ClientID %>").css("display", "none");
            }

            $('#<%= ExpiryType.ClientID %> input').change(function () {
                // The one that fires the event is always the
                // checked one; you don't need to test for this
                $("#<%= ExpiryMonthPanel.ClientID %>").css("display", "none");
                    if ($(this).val() == 1) {
                        $("#<%= ExpiryMonthPanel.ClientID %>").css("display", "block");
                }
            });
        });
    </script>
</asp:Content>