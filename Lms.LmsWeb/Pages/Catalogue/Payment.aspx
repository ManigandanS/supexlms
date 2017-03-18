<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Lms.LmsWeb.Catalogue.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Manage Courses</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" />
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CostText">Cost</asp:Label>
                                <asp:TextBox ID="CostText" runat="server" CssClass="form-control" AutoCompleteType="None" TextMode="Number" disabled="disabled"></asp:TextBox>
                            </div>


                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CardNumber">Card Number</asp:Label>
                                <asp:TextBox ID="CardNumber" runat="server" CssClass="form-control" AutoCompleteType="None" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CardNumber" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="ExpireMonth">Expire Month</asp:Label>
                                        <asp:DropDownList ID="ExpireMonth" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="ExpireYear">Expire Year</asp:Label>
                                        <asp:DropDownList ID="ExpireYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="Cvv2">CVV</asp:Label>
                                        <asp:TextBox ID="Cvv2" runat="server" CssClass="form-control" MaxLength="4" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Cvv2" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div>
                                <asp:Button ID="PayBtn" runat="server" Text="PAY" OnClick="PayBtn_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
