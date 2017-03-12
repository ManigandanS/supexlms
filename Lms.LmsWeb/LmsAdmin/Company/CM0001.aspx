<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CM0001.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Company.CM0001" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Company Details</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CompanyName">Company Name</asp:Label>
                                <p class="form-control-static">
                                    <asp:Literal ID="CompanyName" runat="server"></asp:Literal></p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="TrialCompany">Trial Company</asp:Label>
                                <p class="form-control-static">
                                    <asp:Literal ID="TrialCompany" runat="server"></asp:Literal></p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Expiry">Expiry</asp:Label>
                                <p class="form-control-static">
                                    <asp:Literal ID="Expiry" runat="server"></asp:Literal></p>
                            </div>

                            <div>
                                <a href="CM0201" class="btn btn-primary">BUY PLAN</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
