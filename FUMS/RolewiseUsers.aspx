<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RolewiseUsers.aspx.cs" Inherits="FloraSoft.Cps.UserManager.RolewiseUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/common-script.js"></script>
    <div class="row">
        <div class="col-md-12 xs-zero-padding">
            <div class="top-banner">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
        <div style="text-align:center">
             <h2>Rolewise Users</h2>
        </div>
        <hr />
            <table border="0px">
               
                <tr>
                   
                    <td class="style1">
                        <asp:DropDownList ID="BranchList" CssClass="form-control" runat="server" DataTextField="BranchName" DataValueField="BranchID"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width:15px"></td>
                    <td>
                        <asp:DropDownList ID="ddlRoles" CssClass="form-control" runat="server" DataTextField="RoleName" DataValueField="RoleID"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width:15px"></td>
                    <td>
                        <asp:Button CssClass="btn btn-info" ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />
                    </td>
                </tr>
                
                
            </table>
            <div class="clearfix" style="margin-bottom:15px"></div>

             <asp:GridView CssClass="table table-bordered"  ID="gvwReport" runat="server">
                 <AlternatingRowStyle BackColor="#EFEFEF" />
                 <HeaderStyle BackColor="#CF171F" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
            <!------------------------------------>
            <div>
                <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 383px;
        }
    </style>
</asp:Content>

