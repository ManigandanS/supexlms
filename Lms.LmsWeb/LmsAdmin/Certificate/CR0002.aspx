<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CR0002.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Certificate.CR0002" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Certificate</h2>
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

                            <div class="form-group" style="display: none;" id="expiryMonth">
                                <asp:Label runat="server" AssociatedControlID="ExpiryMonth">Expiry Month</asp:Label>
                                <asp:TextBox ID="ExpiryMonth" runat="server" CssClass="form-control" placeholder="Expiry Month" TextMode="Number" />
                            </div>

                            <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
     <script>
         $(document).ready(function () {
             $('#<%= ExpiryType.ClientID %> input').change(function () {
                // The one that fires the event is always the
                // checked one; you don't need to test for this
                $("#expiryMonth").css("display", "none");
                if ($(this).val() == 1) {
                    $("#expiryMonth").css("display", "block");
                }
            });
        });
    </script>
</asp:Content>