<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserDetail.aspx.cs" Inherits="FloraSoft.Cps.UserManager.UserDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12 xs-zero-padding">
            <div class="top-banner">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
td.ddl {
    position:relative;
    display:inline-block;
    z-index:0
}
td.ddl select {
    z-index:1;
}

td.ddl:before {
    display:block;
    position:absolute;
    content:'';
    right:0px;
    top:0px;
    height:27px;
    width:27px;
    margin:2px;
    background:#ffffff;
    z-index:5;
}
</style>
    <div>
       <div class="row">
            <div class="col-md-4">
                <h2>User Details</h2>
                <table class="table">
                    <tr>
                        <td class="Normal">
                            Branch:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlbranch" runat="server" Width="195px" DataTextField="BranchName"
                                DataValueField="BranchID" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Sub Branch:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubBranch" runat="server" Width="195px" DataTextField="SubBranchName"
                                DataValueField="SubBranchCD" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>                   
                    <tr>
                        <td class="Normal">
                            Login ID:
                            <asp:Label ID="lblMessage" runat="server" CssClass="NormalRed"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtloginid" CssClass="inputlt" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtname" CssClass="inputlt" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Department:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="195px" DataTextField="DeptName"
                                CssClass="Normal" DataValueField="DeptID" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Email:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" CssClass="inputlt" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Contact No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtcontact" CssClass="inputlt" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">All Branch:</td>
                        <td>
                            <asp:CheckBox ID="chkAllBranch" Enabled="false" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-7">
            <fieldset><legend>User Module</legend>
                 <asp:CheckBoxList ID="UMRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID" Enabled="false"></asp:CheckBoxList>
            </fieldset>
            <br />
            <asp:Panel ID="RTGSPanel" Visible="false" runat="server">
            <fieldset><legend>RTGS</legend>
                  <asp:CheckBoxList ID="RTGSRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID" Enabled="false"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
            <br />
            <asp:Panel ID="CPSPanel" Visible="false" runat="server">
            <fieldset><legend>CPS</legend>
                  <asp:CheckBoxList ID="CPSRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID" Enabled="false"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
            <br />
            <asp:Panel ID="EFTNPanel" Visible="false" runat="server">
            <fieldset><legend>EFTN</legend>
                  <asp:CheckBoxList ID="EFTNRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID" Enabled="false"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
                     <asp:Panel ID="ZonePanel" Visible="false" runat="server">
            <fieldset><legend>Zone</legend>
                  <asp:CheckBoxList ID="ZoneList" runat="server" CellPadding="5" DataTextField="ZoneName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="ZoneID" Enabled="false"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
            <br />
            </div>
        </div>
    </div>
</asp:Content>
