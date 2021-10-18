<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FloraSoft.Cps.UserManager.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>    
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Style CSS -->
    <link href="Content/Site.css" rel="stylesheet" />
    <style type="text/css">
        h3
        {
            font-size: 18px;
            top: 0px;
            left: 0px;
        }
        h3
        {
            letter-spacing: 0px;
            font-weight: normal;
            position: relative;
            padding: 0 0 10px 0;
            font-weight: normal;
            font-family: "Helvetica Neue" ,Helvetica,Arial,sans-serif;
            line-height: 140% !important;
            color: #01090c;
        }
        h3
        {
            margin-top: 20px;
            margin-bottom: 10px;
        }
        .pull-right
        {
            float: right;
        }
        
            .input-group .form-control:last-child, .input-group-addon:last-child, .input-group-btn:last-child > .btn, .input-group-btn:last-child > .dropdown-toggle, .input-group-btn:first-child > .btn:not(:first-child) {
              border-bottom-left-radius: 0;
              border-top-left-radius: 0;
            }
           
  
        
    </style>
</head>
<body style="background-image: url(Images/bg_3.png); background-color: rgba(255, 255, 255, 0);">
    <form  id="form1" method="post"  runat="server" defaultbutton="LoginBtn" defaultfocus="UserName">
        <div class="row">
            <div class="col-md-12">
            <div class="login">
                <div class="main-login col-md-4 col-md-offset-4">
                    <div class="loginback">
                        <!-- start: LOGIN BOX -->
                        <div id="Div1" class="box-login" runat="server">
                            <div style="text-align: center">
                                <div>
                                    <img alt="logo" src="images/logo.png " style="max-width:200px" />
                                </div>
                                <h3>Flora User Profile Management System</h3>

                            </div>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <fieldset>
                               <div class="form-horizontal">
                                   <div class="form-group">
                                        <label for="" class="col-sm-3 control-label">User Name</label>
                                        <div class="col-sm-6" style="vertical-align:top">
                                            <asp:TextBox id="txtUserID"  placeholder="Login ID"  CssClass="form-control"  runat="server" />
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator1"  runat="server" ControlToValidate="txtUserID"  CssClass="NormalRed pull-right"  ErrorMessage="*" Display="dynamic" />
                                        </div>                                    
                                        <div class="col-sm-3"></div>
                                   </div>
                                   <div class="form-group">
                                        <label for="" class="col-sm-3 control-label">Password</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox id="txtPass"  CssClass="form-control"  autocomplete="off" placeholder="Password" TextMode="Password" runat="server" />
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator2"  runat="server" ControlToValidate="txtPass"   CssClass="NormalRed pull-right" ErrorMessage="*" Display="dynamic" />
                                       </div>                                    
                                       <div class="col-sm-3">
                                           <asp:LinkButton ID="LoginBtn" CssClass="btn btn-info marginleft 20" Runat="server" Text="Sign In " OnClick="btnLogin_Click" ></asp:LinkButton>
                                       </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="MyMessage" ForeColor="Red" CssClass ="NormalRed" runat="server"></asp:Label> 
                                    <asp:HiddenField ID="Tried" Value="" runat="server" />
                                    <br />
                                   <asp:Panel CssClass="errorHandler alert alert-danger no-display" ID="pnlLoginMessage" runat="server">
                                        <span class="glyphicon glyphicon-asterisk" aria-hidden="true"></span>
                                        <asp:Label ID="lblLoginMessage" runat="server"></asp:Label>
                                    </asp:Panel>
                                </div>
                            </fieldset>
                            <div style="text-align:center">
                                <p>Powered by <span><a href="http://www.floralimited.com" style="color:Red">Flora Limited</a></span></p>
                            </div>            
                          </div>
                        <!-- end: LOGIN BOX -->

                    </div>
                </div>
            </div>

        </div>
        </div>
    </form>
    <script src="Scripts/modernizr-2.6.2.js" type="text/javascript"></script>
    <script src="Scripts/html5.js" type="text/javascript"></script>
    <script src="Scripts/respond.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

</body>
</html>
