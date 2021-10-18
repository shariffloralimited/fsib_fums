<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StatuswiseUsers.aspx.cs" Inherits="FloraSoft.Cps.UserManager.StatuswiseUsers" %>

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
        <h2 align=center> Statuswise Users</h2>
        <hr />
            <table border="0px" style="margin-bottom:15px">               
                <tr>
                    
                    <td class="style1">
                        <asp:Label ID="Label1" runat="server" Text="Branch Name:" Style="font-weight: 700"></asp:Label>
                        <asp:DropDownList ID="BranchList" CssClass="form-control" runat="server" DataTextField="BranchName" DataValueField="BranchID">
                        </asp:DropDownList>
                    </td>
                    <td style="width:15px"></td>
                    <td>
                    <div style="margin-top:15px">
                        <asp:Button ID="btnActive" CssClass="btn btn-success" runat="server" Text="Active Users" OnClick="btnActive_Click"  />
                    </div>
                    </td>
                    <td style="width:15px"></td>
                     <td>
                     <div style="margin-top:15px">
                      <asp:Button ID="btnInactive" CssClass="btn btn-danger" runat="server" Text="Inactive Users" OnClick="btnInactive_Click"  />
                     </div>
                    </td>
                    <td style="width:15px"></td>
                      <td>  
                      <div style="margin-top:15px">
                      <asp:Button ID="btnSuspended" CssClass="btn btn-info" runat="server" Text="Locked Users" OnClick="btnSuspended_Click" />
                      </div>
                    </td>
                    <td style="width:15px"></td>
                      <td>  
                      <div style="margin-top:15px">
                      <asp:Button ID="btnPending" CssClass="btn btn-warning" runat="server" Text="Pending Users" OnClick="btnPending_Click" />
                      </div>
                    </td>
                    <td style="width:15px"></td>
                      <td>  
                      <div style="margin-top:15px">
                      <asp:Button ID="btnDisapproved" CssClass="btn btn-primary" runat="server" Text="Rejected Users" OnClick="btnDisapproved_Click" />
                      </div>
                    </td>                    
                    <td style="width:15px"></td>
                      <td>  
                      <div style="margin-top:15px">
                      <asp:Button CssClass="btn btn-success"  ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />
                      </div>
                    </td>
                    
                </tr>              
            </table>
             <asp:GridView CssClass="table table-bordered table-hover"  ID="gvwReport" runat="server">
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
