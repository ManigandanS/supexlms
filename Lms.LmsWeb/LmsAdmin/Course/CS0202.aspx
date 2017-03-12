<%@ Page Language="C#" MasterPageFile="~/LmsAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="CS0202.aspx.cs" Inherits="Lms.LmsWeb.LmsAdmin.Course.CS0202" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Create Session</h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">

                    <div class="row">
                        <div class="col-xs-12">
                            <div>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="text-danger validation-summary" Display="Dynamic" />
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="CourseName">Course Name</asp:Label>
                                <p class="form-control-static">
                                    <asp:Literal ID="CourseName" runat="server"></asp:Literal>
                                </p>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="SessionName">Session Name</asp:Label>
                                <asp:TextBox ID="SessionName" runat="server" PlaceHolder="Session Name" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="SessionName" ErrorMessage="Required" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Description">Description</asp:Label>
                                <asp:TextBox ID="Description" runat="server" PlaceHolder="Description" CssClass="form-control" AutoCompleteType="None" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Cost">Cost (USD)</asp:Label>
                                <asp:TextBox ID="Cost" runat="server" PlaceHolder="Cost" CssClass="form-control" AutoCompleteType="None" TextMode="Number" Text="0"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="SessionDate">Session Date</asp:Label>
                                <div class="control-group">
                                    <div class="controls">
                                        <div class="input-prepend input-group">
                                            <span class="add-on input-group-addon"><i class="glyphicon glyphicon-calendar fa fa-calendar"></i></span>
                                            <asp:TextBox ID="SessionDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="EnrollDate">Enroll Date</asp:Label>
                                <div class="control-group">
                                    <div class="controls">
                                        <div class="input-prepend input-group">
                                            <span class="add-on input-group-addon"><i class="glyphicon glyphicon-calendar fa fa-calendar"></i></span>
                                            <asp:TextBox ID="EnrollDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <asp:Panel ID="OnlineSession" runat="server" Visible="false">
                            </asp:Panel>



                            <asp:Panel ID="OfflineSession" runat="server" Visible="false">
                            </asp:Panel>


                            <div>
                                <asp:Button ID="CreateBtn" runat="server" Text="Create" CssClass="btn btn-default" OnClick="CreateBtn_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>


<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">



    <script>

        $('#<%= SessionDate.ClientID %>').daterangepicker({
            timePicker: true,
            timePickerIncrement: 10,
            locale: {
                format: 'MM/DD/YYYY h:mm A'
            }
        });

        $('#<%= EnrollDate.ClientID %>').daterangepicker({
            timePicker: true,
            timePickerIncrement: 10,
            locale: {
                format: 'MM/DD/YYYY h:mm A'
            }
        });

    </script>

</asp:Content>
