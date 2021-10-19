<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="FloraSoft.Cps.UserManager.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12 xs-zero-padding">
            <div class="top-banner"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
           <div style="text-align:center"><h2>User Management</h2><hr /></div>
           <div class="row" >
               <div class="col-md-2" style="float:left">
                    <asp:DropDownList ID="ddlModule" CssClass="form-control" runat="server" DataTextField="ModuleName" DataValueField="ModuleID"  AutoPostBack="true"  />
               </div>
               <div class="col-md-3" style="float:left">
                    <asp:DropDownList ID="BranchList" CssClass="form-control" runat="server" DataTextField="BranchName" DataValueField="BranchID"  AutoPostBack="true" />
               </div>
               <div class="col-md-2" style="float:left">
                    <asp:DropDownList ID="StatusList" CssClass="form-control" runat="server" DataTextField="StatusName" DataValueField="StatusID"  AutoPostBack="true" />
               </div>
               <div class="col-md-2" style="float:left">
                  <div style="padding-top:8px;font-weight:normal"><asp:CheckBox ID="ChkIsPending" style="font-weight:normal" Text="&nbsp;Pending" AutoPostBack="true" runat="server" /></div>
               </div>
               <div class="col-md-2" style="float:left">
                    <asp:TextBox ID="txtLoginID" placeholder="Find by Login ID" runat="server" CssClass="form-control" />
                </div>           
               <div class="col-md-1" style="float:left">
                    <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-success" />
                </div> 
           </div>
           <br />
           <br />
            <!------------------------------------>
            <asp:DataGrid ID="MyDataGrid2" CssClass="table table-bordered" runat="server"
                AutoGenerateColumns="False" DataKeyField="UserID" ShowFooter="True" OnItemCommand="MyDataGrid2_ItemCommand">
                <AlternatingItemStyle BackColor="#EFEFEF" />
                <Columns>
                    <asp:EditCommandColumn CausesValidation="False" Visible="true" EditText="Edit" UpdateText="Update"
                        CancelText="Cancel"></asp:EditCommandColumn>
                    <asp:EditCommandColumn CausesValidation="False" Visible="false" EditText="Approve" UpdateText="Update"
                        CancelText="Cancel"></asp:EditCommandColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkActive" runat="server" />
                            <asp:Label Text='<%#DataBinder.Eval(Container.DataItem,"UserID") %>' ID="LabelUserID" Visible ="false" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetail" CommandName = "Detail" Text="Detail" runat = "server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:HyperLinkColumn DataNavigateUrlField="UserID" Text="Reset" DataNavigateUrlFormatString="ResetPassword.aspx?UserID={0}" />
                  <%--   <asp:HyperLinkColumn DataNavigateUrlField="UserID" HeaderText="Login Mins" HeaderStyle-Width="90px" DataTextField="LoginMins" DataNavigateUrlFormatString="UserLogout.aspx?UserID={0}" />--%>
                   <%-- <asp:TemplateColumn HeaderText="Login Mins">
                        <ItemTemplate>
                            <a href='UserLogout.aspx?UserID=<%#DataBinder.Eval(Container.DataItem, "UserID")%>&ModuleID=<%#DataBinder.Eval(Container.DataItem, "ModuleID")%>'><%#DataBinder.Eval(Container.DataItem, "LoginMins")%></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
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
                    <asp:TemplateColumn HeaderText="Branch">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sub Branch">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "SubBranchName")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Contact No.">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "ContactNo")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status">
                        <ItemTemplate>                            
                                <%#DataBinder.Eval(Container.DataItem, "StatusName")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Pending">
                        <ItemTemplate>                            
                                <%#DataBinder.Eval(Container.DataItem, "IsPending")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Logged In">
                     <ItemTemplate>
                            <a href='UserLogout.aspx?UserID=<%#DataBinder.Eval(Container.DataItem, "UserID")%>&ModuleID=<%#DataBinder.Eval(Container.DataItem, "ModuleID")%>'><%#DataBinder.Eval(Container.DataItem, "IsLoggedIn")%></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
             <%--       <asp:TemplateColumn HeaderText="All Branch">
                        <ItemTemplate>                            
                                <%#DataBinder.Eval(Container.DataItem, "AllBranch")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                 <%--       
                    <asp:HyperLinkColumn DataNavigateUrlField="UserID" Text="Reset" DataNavigateUrlFormatString="ResetPassword.aspx?UserID={0}" />--%>
                    <asp:TemplateColumn Visible ="false">
                        <ItemTemplate>
                            <a href="ActivateUsers.aspx?UserID=<%#DataBinder.Eval(Container.DataItem, "UserID")%>">
                                Change Status</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#CF171F" Font-Bold="True" ForeColor="White" />
            </asp:DataGrid>
            <div class="col-md-5">
                 <div class="col-md-2">
                    <div style="float:left;margin-top:2px"><asp:CheckBox runat="server" ID="chkCheckAll" AutoPostBack="true" /></div>
                 </div>
                 <div class="col-md-10">
                     <div id="MakerPanel" runat="server">
                        <div style="float:left;">
                            <asp:DropDownList ID="ddlStatus1" CssClass="form-control" runat="server" DataTextField="StatusName" DataValueField="StatusID" />
                        </div>
                        <div style="float:left;margin-left:15px">
                            <asp:Button Text="Update" ID="btnChangeStatus" CssClass="btn btn-success" runat="server" OnClick="btnChangeStatus_Click" />
                        </div>
                    </div>
                    <!--------------------------->
                    <div id="CheckerPanel" visible="false" style="float:left" runat="server">
                        <div style="float:left;">
                            <asp:Button Text="Approve" ID="btnAprove" CssClass="btn btn-success" runat="server" OnClick="btnAprove_Click" />
                        </div>
                        <div style="float:left;margin-left:15px">
                            <asp:Button Text="Reject" ID="btnDisapprove" CssClass="btn btn-danger" runat="server" OnClick="btnDisapprove_Click"  />
                        </div>                   
                    </div>
            </div>
    
           <div>
                <asp:Label runat="server" ID="lblTestCookie"></asp:Label>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
