<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubBranch.aspx.cs" Inherits="FloraSoft.Cps.UserMgr.SubBranch" %>

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
                <p align="center" class="Head">
                    <asp:Label ID="lblErrMsg" runat="server"></asp:Label>
                </p>
                <br />
                <h2 align="center">Sub Branch Listings</h2>
            </div>
            <div align="center">

                <asp:DataGrid CssClass="table table-bordered" ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                    ItemStyle-CssClass="NormalSmall"
                    runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="false" DataKeyField="SubBranchID"
                    GridLines="None" BorderWidth="0px" ShowFooter="true" Height="0px"
                    OnItemCommand="MyDataGrid_ItemCommand" Width="674px">
                    <Columns>
                        <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                            CancelText="Cancel">
                            <FooterStyle CssClass="red"></FooterStyle>
                            <ItemStyle CssClass="CommandButton" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Sub Branch Name">
                            <FooterTemplate>
                                <asp:TextBox ID="addSubBranchName" MaxLength="40" runat="Server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddSubBranchName" CssClass="NormalRed"
                                    runat="server" ControlToValidate="addSubBranchName" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "SubBranchName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="SubBranchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubBranchName") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubBranchName" CssClass="NormalRed"
                                    runat="server" ControlToValidate="SubBranchName" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sub Branch Code">
                            <FooterTemplate>
                                <asp:TextBox ID="addSubBranchCD" MaxLength="4" runat="Server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddSubBranchCD" CssClass="NormalRed"
                                    runat="server" ControlToValidate="addSubBranchCD" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "SubBranchCD")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="SubBranchCD" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubBranchCD") %>'
                                    MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Branch Name">
                            <FooterTemplate>
                                <asp:DropDownList ID="addDdlBranch" runat="server" Width="195" style="height:22px" DataTextField="BranchName"
                                    CssClass="Normal" DataValueField="RoutingNo" DataSource='<%#LoadBranchDdlData() %>'>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddDdlBranch" CssClass="NormalRed"
                                    runat="server" ControlToValidate="addDdlBranch" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "BranchName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="editDdlBranch" runat="server" Width="195" style="height:22px" DataTextField="BranchName" DataSource='<%#LoadBranchDdlData() %>' SelectedValue='<%#DataBinder.Eval(Container.DataItem, "RoutingNo")%>'
                                    CssClass="Normal" DataValueField="RoutingNo">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEdditDdlBranch" CssClass="NormalRed"
                                    runat="server" ControlToValidate="editDdlBranch" ErrorMessage="*" Display="dynamic">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <FooterStyle CssClass="red" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
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
                <%--<p align="center" class="Head"><asp:Label ID="lblErrMsg" runat="server"></asp:Label></p>--%>
            </div>

        </div>
    </div>
</asp:Content>
