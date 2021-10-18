<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChangePassword.aspx.cs"
    Inherits="FloraSoft.Cps.UserManager.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12 xs-zero-padding">
            <div class="top-banner">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Change My Password</h2>
        <div class="row">
            <div class="col-md-4">
                <table class="table table-hover" style="border: none">
                    <tr>
                        <td class="Normal" width="150">Old Password:</td>
                        <td><asp:TextBox ID="OldPassword" TextMode="Password" CssClass="inputlt" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="ReqOldPassword" runat="server" Display="Dynamic" 
                            ControlToValidate="OldPassword" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="Normal">New Password:</td>
                        <td><asp:TextBox ID="NewPassword" TextMode="Password" CssClass="inputlt" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="ReqNewPassword" runat="server" Display="Dynamic" 
                            ControlToValidate="NewPassword" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="Normal">Confirm Password:</td>
                        <td><asp:TextBox ID="ConfirmPassword" TextMode="Password" CssClass="inputlt" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="ReqConfirmPassword" runat="server" Display="Dynamic" 
                            ControlToValidate="ConfirmPassword" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
                        <td colspan="2"><asp:Label ID="lblMsg" runat="server" CssClass="alert-success" /></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
