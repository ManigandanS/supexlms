<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonSearch.ascx.cs" Inherits="Lms.LmsWeb.UserControls.CommonSearch" %>


<section class="search-section">
    <div class="row">
        <div class="col-sm-10" style="padding-bottom: 4px;">
            <asp:TextBox ID="SearchKeyword" runat="server" CssClass="form-control" placeholder="Search Keyword"></asp:TextBox>
        </div>
        <div class="col-sm-2" style="padding-bottom: 4px;">
            <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="btn btn-default btn-block" OnClick="SearchBtn_Click" />
        </div>
    </div>
</section>
