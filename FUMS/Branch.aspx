<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Branch.aspx.cs" Inherits="FloraSoft.Cps.UserMgr.Branch" MasterPageFile="~/Site.Master" Title="Branch" %>
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
            <div>
                <h2 align="center">Branch Listings</h2>
            </div>
                 <div>
                            <asp:DropDownList style="visibility:hidden" ID="ddlZone" runat="server" Width="195px" DataTextField="ZoneName"
                                CssClass="Normal" DataValueField="ZoneID" AutoPostBack="true" />
                            <%--</asp:DropDownList>--%>
                        </div>
            <div align="center">

                        <asp:DataGrid CssClass="table table-bordered" ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall"
                            runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="false" DataKeyField="BranchID"
                            GridLines="None" BorderWidth="0px"  ShowFooter="true" Height="0px"  
                            OnItemCommand="MyDataGrid_ItemCommand" Width="674px">
                            <Columns>
                                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                                    CancelText="Cancel">
                                    <FooterStyle CssClass="red"></FooterStyle>
                                    <ItemStyle CssClass="CommandButton" />
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="Branch Name">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBranchName"  MaxLength="40" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddBranchName" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBranchName" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="BranchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BranchName") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBranchName" CssClass="NormalRed"
                                            runat="server" ControlToValidate="BranchName" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Routing No">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addRoutingNo" Width="65" MaxLength="9" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddRoutingNo" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addRoutingNo" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "RoutingNo")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="RoutingNo" Width="75" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RoutingNo") %>'
                                            MaxLength="9"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Branch Numonic">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBranchNumonic" Width="65" MaxLength="9" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddBranchNumonic" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBranchNumonic" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BranchNumonic")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="BranchNumonic" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BranchNumonic") %>'
                                            MaxLength="9"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Branch Code">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addBranchCD" Width="65" MaxLength="4" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddBranchCD" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addBranchCD" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BranchCD")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="BranchCD" Width="65" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BranchCD") %>'
                                            MaxLength="4"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                       <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Approved")%>
                                      </ItemTemplate>
                                      <EditItemTemplate>
                                        <asp:CheckBox ID="ChkApproved" Width="65" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"ApprovedBoolean") %>' ></asp:CheckBox>
                                      </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton CommandName="Insert" Text="Add" ID="btnAdd" CssClass="btn btn-success" ForeColor="White" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle CssClass="GrayBackWhiteFont" />
                            <AlternatingItemStyle BackColor="#EFEFEF" />
                            
                            <HeaderStyle BackColor="#CF171F" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid>
                        <br />
                        <p align="center" class="Head"><asp:Label ID="lblErrMsg" runat="server"></asp:Label></p>
        </div>
           
        </div>
    </div>
</asp:Content>