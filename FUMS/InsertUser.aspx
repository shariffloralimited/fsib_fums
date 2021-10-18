<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="true" CodeBehind="InsertUser.aspx.cs" Inherits="FloraSoft.Cps.UserManager.InsertUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/common-script.js"></script>
    <div class="row">
        <div class="col-md-12 xs-zero-padding">
            <div class="top-banner"></div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
   
        <div class="col-md-4">
             <h2>Add User</h2>
            <table Class="table table-hover" style="border:none">
                <tr>
                   
                    <td class="Normal">Branch:</td>
                    <td>
                        <asp:DropDownList ID="ddlbranch" runat="server" Width="195px" style="height:25px" DataTextField="BranchName" DataValueField="RoutingNo"></asp:DropDownList>
                    </td>
                </tr>                     
                <tr>
                   
                    <td class="Normal">Login ID:</td>
                    <td>
                        <asp:TextBox ID="txtloginid" CssClass="inputlt" MaxLength="20" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="LoginIdFieldvalidator" runat="server" Display="Dynamic" 
                            ControlToValidate="txtloginid" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExpReasonForPayment" runat="server" CssClass="normal-red" 
                             ErrorMessage="Invalid Characters" Display="Dynamic" ControlToValidate="txtloginid" ValidationExpression="^[a-zA-Z0-9.-]+$" />
                    </td>
                </tr>
                     <tr>
                   
                    <td class="Normal">Password:</td>
                    <td>
                        <asp:TextBox ID="txtpass" CssClass="inputlt" TextMode="Password"  MaxLength="20" EnableViewState="true" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordFieldValidator" runat="server" Display="Dynamic"
                            ControlToValidate="txtpass" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                   
                    <td class="Normal">Name:</td>
                    <td>
                        <asp:TextBox ID="txtname" CssClass="inputlt"  MaxLength="40" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UsernameFieldvalidator" runat="server" Display="Dynamic"
                            ControlToValidate="txtname" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>             
                    <td class="Normal">Department:</td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" style="height:25px" DataTextField="DeptName" DataValueField="DeptID"></asp:DropDownList>
                    </td>
                </tr>
                <tr>             
                    <td class="Normal">Status:</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" style="height:25px" DataTextField="StatusName" DataValueField="StatusID"></asp:DropDownList>
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
                    <td class="Normal">Contact No:</td>
                    <td>
                        <asp:TextBox ID="txtcontact" CssClass="inputlt" MaxLength="20"  runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ContactFieldvalidator" runat="server" Display="Dynamic"
                            ControlToValidate="txtcontact" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ContactRegExValidator"  Display="Dynamic"
                            runat="server" ErrorMessage="Enter valid Phone number" 
                            ControlToValidate="txtcontact" 
                            ValidationExpression= "^([0-9\(\)\/\+ \-]*)$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                 
                    <td class="Normal">All Branch:</td>
                    <td>
                        <asp:CheckBox ID="chkAllBranch" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                   
                    <td colspan="2">
                        <asp:Button
                            ID="btnInsert" CssClass="inputlt" runat="server" Text="Add User" OnClick="btnInsert_Click"
                            Height="26px" /><br />
                        <asp:Label ID="lblMessage" runat="server" CssClass="alert-danger"></asp:Label>
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
            <br />
        </div>
    </div>

</asp:Content>
