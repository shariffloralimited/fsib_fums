<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EditUser.aspx.cs"
    Inherits="FloraSoft.Cps.UserManager.EditUser" %>

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
        <div class="row">
            <div class="col-md-4">
                                  <h2> Edit User</h2>
                <table class="table table-hover" style="border: none">
                    <tr>
                        <td class="Normal">
                            Branch:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlbranch" runat="server" Width="195px" DataTextField="BranchName" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"
                                CssClass="Normal" DataValueField="RoutingNo">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Sub Branch:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubBranch" runat="server" Width="195px" DataTextField="SubBranchName" CssClass="Normal" DataValueField="SubBranchCD">
                                <asp:ListItem Text="--SELECT--" Value="" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Login ID:
                        </td>
                        <td>
                            <asp:TextBox ID="txtloginid" CssClass="inputlt"  MaxLength="20" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LoginIdFieldvalidator" runat="server" 
                                ControlToValidate="txtloginid" Display="Dynamic"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExpReasonForPayment" runat="server" 
                                CssClass="normal-red"  ErrorMessage="Invalid Characters" Display="Dynamic" 
                                ControlToValidate="txtloginid" ValidationExpression="^[a-zA-Z0-9.]+$" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtname" CssClass="inputlt"  MaxLength="40" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UsernameFieldvalidator" runat="server" 
                                ControlToValidate="txtname" Display="Dynamic"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
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
                    <tr>
                        <td class="Normal">
                            Status:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="195px" DataTextField="StatusName"
                                CssClass="Normal" DataValueField="StatusID" />
                        </td>
                    </tr>
                    <tr>
                    <td class="Normal">Email:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" CssClass="inputlt" MaxLength="40"  runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailValidator" runat="server" Display="Dynamic"
                            ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="EmailExpressionValidator" 
                            runat="server" ErrorMessage="Enter  a valid Email" 
                            ControlToValidate="txtEmail" Display="Dynamic" 
                            ValidationExpression= "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                    <tr>
                        <td class="Normal">
                            Contact No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtcontact" CssClass="inputlt"  MaxLength="20" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ContactFieldvalidator" runat="server" Display="Dynamic"
                            ControlToValidate="txtcontact" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="ContactRegExValidator"  Display="Dynamic"
                                runat="server" ErrorMessage="Enter valid Phone number" 
                                ControlToValidate="txtcontact" 
                                ValidationExpression= "^([0-9\(\)\/\+ \-]*)$"></asp:RegularExpressionValidator>                            
                        </td>
                    </tr>
                    <tr>
                        <td class="Normal">
                            All Branch:
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkAllBranch" runat="server" />  
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Label ID="lblMessage" style=":blue" runat="server"  CssClass="NormalRed"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
                                OnClick="btnUpdate_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-7">
            <fieldset><legend>User Module</legend>
                 <asp:CheckBoxList ID="UMRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID"></asp:CheckBoxList>
            </fieldset>
            <br />
            <asp:Panel ID="RTGSPanel" Visible="false" runat="server">
            <fieldset><legend>RTGS</legend>
                  <asp:CheckBoxList ID="RTGSRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
            <br />
            <asp:Panel ID="CPSPanel" Visible="false" runat="server">
            <fieldset><legend>CPS</legend>
                  <asp:CheckBoxList ID="CPSRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
            <br />
            <asp:Panel ID="EFTNPanel" Visible="false" runat="server">
            <fieldset><legend>EFTN</legend>
                  <asp:CheckBoxList ID="EFTNRoleList" runat="server" CellPadding="5" DataTextField="RoleName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="RoleID"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
               <asp:Panel ID="ZonePanel" Visible="false" runat="server">
            <fieldset><legend>Zone</legend>
                  <asp:CheckBoxList ID="ZoneList" runat="server" CellPadding="5" DataTextField="ZoneName" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="Normal" DataValueField="ZoneID"></asp:CheckBoxList>
            </fieldset>
            </asp:Panel>
            <br />
            </div>
        </div>
    </div>
</asp:Content>
