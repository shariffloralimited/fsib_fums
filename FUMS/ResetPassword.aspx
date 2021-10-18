<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ResetPassword.aspx.cs"
    Inherits="FloraSoft.Cps.UserManager.ResetPassword" %>

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
        <h2>Reset User Password</h2>
        <div class="row">
            <div class="col-md-4">
                <table class="table table-hover" style="border: none">
                    <tr>
                        <td class="Normal">Name:</td>
                        <td>
                            <asp:Label ID="lblname" CssClass="inputlt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">Login ID:</td>
                        <td>
                            <asp:Label ID="lblLoginID" CssClass="inputlt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">New Password:</td>
                        <td><asp:TextBox ID="txtPassword" TextMode="Password" MaxLength="20"  CssClass="inputlt" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Label ID="lblMsg" runat="server" CssClass="alert-success" /></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
