<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserChanges.aspx.cs" Inherits="FloraSoft.Cps.UserManager.UserChanges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
             <h2>
                User Management</h2>
                <hr />
           </div>   
            <table border="0px">
                <tr>                    
                    <td style="width:300px">
                        <strong>Branch Name:</strong>
                        <asp:DropDownList ID="BranchList" runat="server" CssClass="form-control" DataTextField="BranchName" DataValueField="BranchID"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    
                    <td>
<%--                       <div style="float:right;margin-top:15px;">
                         <a href="Users.aspx?BranchListID=<%= BranchList.SelectedValue %>" class="btn btn-success">
                            Approved Users</a>
                       </div>--%>
                       <div style="margin-top:15px; float:right">
                        <a href="Users.aspx?BranchListID=<%= BranchList.SelectedValue %>" class="btn btn-success">
                             Approved Users</a>
                       </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <!------------------------------------>
            <asp:DataGrid ID="MyDataGrid2" CssClass="table table-bordered" runat="server"
                AutoGenerateColumns="False" DataKeyField="UserID" ShowFooter="True" OnItemCommand="MyDataGrid2_ItemCommand">
                <AlternatingItemStyle BackColor="#EFEFEF" />
                <Columns>
                    <asp:EditCommandColumn CausesValidation="False" Visible="false"   EditText="Edit" UpdateText="Update"
                        CancelText="Cancel"></asp:EditCommandColumn>
                    <asp:EditCommandColumn CausesValidation="False" Visible="false" EditText="Detail"
                        UpdateText="Update" CancelText="Cancel"></asp:EditCommandColumn>
                    <asp:TemplateColumn HeaderText="Login ID">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "LoginID")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Name">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Department">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Department")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Contact No.">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ContactNo")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status">
                        <ItemTemplate>                            
                                <%#DataBinder.Eval(Container.DataItem, "AccountStatus")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="" Visible ="false">
                        <ItemTemplate>
                            <a href="ActivateUsers.aspx?UserID=<%#DataBinder.Eval(Container.DataItem, "UserID")%>">
                                Change Status</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#CF171F" Font-Bold="True" ForeColor="White" />
            </asp:DataGrid>
            <div>
                <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
