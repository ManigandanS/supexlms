<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CN0003.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Content.CN0003" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Edit Content</h2>
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
                                <asp:Label runat="server" AssociatedControlID="Manifest">Manifest</asp:Label>
                                <pre><code><asp:Literal ID="Manifest" runat="server"></asp:Literal></code></pre>
                            </div>

                            <asp:Button ID="EditBtn" runat="server" Text="Save Changes" CssClass="btn btn-default" OnClick="EditBtn_Click" />
                            <asp:Button ID="DelBtn" runat="server" OnClick="DelBtn_Click" Text="Delete" CssClass="btn btn-danger" OnClientClick="return confirmDelete();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        function confirmDelete() {
            return confirm('You are about to delete the content. Are you sure to delete this content?');
        }
    </script>

</asp:Content>
