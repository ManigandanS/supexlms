<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Lms.LmsWeb.Account.Login" %>


<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - My ASP.NET Application</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <!-- Bootstrap -->
    <link href="/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="/vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- Animate.css -->
    <link href="/vendors/animate.css/animate.min.css" rel="stylesheet">

    <!-- Custom Theme Style -->
    <link href="/Content/custom.css" rel="stylesheet">

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <style>
        

        #office365-login span {
            font-size: 18px;
            font-weight: normal;
            display: inline-block;
            vertical-align: middle;
        }
    </style>
</head>

<body class="login">
    <div>
        <a class="hiddenanchor" id="signup"></a>
        <a class="hiddenanchor" id="signin"></a>

        <div class="login_wrapper">
            <div class="animate form login_form">
                <section class="login_content">
                    <form runat="server">
                        <h1>Login Form</h1>
                        <div>
                            <asp:TextBox ID="UserName" runat="server" placeholder="USERNAME" CssClass="form-control input-lg" />
                        </div>
                        <div>
                            <asp:TextBox ID="Password" runat="server" placeholder="PASSWORD" CssClass="form-control input-lg" TextMode="Password" />
                        </div>
                        <div>
                            <asp:Button ID="LoginBtn" runat="server" Text="Sign In" OnClick="LoginBtn_Click" CssClass="btn btn-primary btn-block btn-lg" style="margin-left: 0;" />                            
                        </div>
                        <div>
                            <a class="reset_pass" href="#">Lost your password?</a>
                        </div>

                        <div class="clearfix"></div>
                        
                        <div class="separator"></div>
                        <asp:Panel ID="Office365SignIn" runat="server" Visible="false">                            
                            <a id="office365-login" href="/account/azure" class="btn btn-default btn-block btn-lg"><img src='/images/office365.png' alt='office 365 logo' style="width: 28px;" />&nbsp;<span>Office 365 Sign In</span></a>
                        </asp:Panel>
                        <asp:Panel ID="AdfsSignIn" runat="server" Visible="false">
                            <a class="btn btn-default btn-block btn-lg"><i class="fa fa-windows" aria-hidden="true"></i>&nbsp;Intranet Sign In</a>
                        </asp:Panel>
                        
                        <div class="clearfix"></div>

                        <div class="separator">
                            <p class="change_link">
                                New to site?
                  <a href="#signup" class="to_register">Create Account </a>
                            </p>

                            <div class="clearfix"></div>
                            <br>
                            
                        </div>
                    </form>
                </section>
            </div>

            <div id="register" class="animate form registration_form">
                <section class="login_content">
                    <form>
                        <h1>Create Account</h1>
                        <div>
                            <input type="text" class="form-control" placeholder="Username" required="">
                        </div>
                        <div>
                            <input type="email" class="form-control" placeholder="Email" required="">
                        </div>
                        <div>
                            <input type="password" class="form-control" placeholder="Password" required="">
                        </div>
                        <div>
                            <a class="btn btn-default submit" href="index.html">Submit</a>
                        </div>

                        <div class="clearfix"></div>

                        <div class="separator">
                            <p class="change_link">
                                Already a member ?
                  <a href="#signin" class="to_register">Log in </a>
                            </p>

                            <div class="clearfix"></div>
                            <br>

                            <div>
                                <h1><i class="fa fa-paw"></i>Gentelella Alela!</h1>
                                <p>©2016 All Rights Reserved. Gentelella Alela! is a Bootstrap 3 template. Privacy and Terms</p>
                            </div>
                        </div>
                    </form>
                </section>
            </div>
        </div>
    </div>


</body>
</html>
