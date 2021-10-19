<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TransactionLimit.aspx.cs" Inherits="FloraSoft.Cps.UserManager.TransactionLimit" %>

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
                <h2 align="center" class="Head">
                    Transaction Limit</h2>
                <p align="center" class="Head">
                        <br />
                                    <asp:Label ID="lblErrMsg" runat="server"></asp:Label>
                        </p>
            </div>
            <hr />

            <div align="center">
            <asp:DataGrid CssClass="table table-bordered" ID="MyDataGrid" HeaderStyle-CssClass="GrayBackWhiteFont" FooterStyle-CssClass="GrayBackWhiteFont"
                            ItemStyle-CssClass="NormalSmall"
                            runat="server" CellSpacing="1" CellPadding="5" AutoGenerateColumns="false" DataKeyField="LevelID"
                            GridLines="None" BorderWidth="0px"  ShowFooter="true" Height="0px"  
                            OnItemCommand="MyDataGrid_ItemCommand" Width="674px">
                            <Columns>
                                <asp:EditCommandColumn CausesValidation="False" EditText="Edit" ItemStyle-Width="30px" UpdateText="Update"
                                    CancelText="Cancel">
                                    <FooterStyle CssClass="red"></FooterStyle>
                                    <ItemStyle CssClass="CommandButton" />
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="Level" HeaderStyle-Width="150">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addLevelName" MaxLength="15" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLevelName" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addLevelName" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "LevelName")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="LevelName"  MaxLength="15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LevelName") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLevelName1" CssClass="NormalRed"
                                            runat="server" ControlToValidate="LevelName" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TransLimit"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addTransLimit" Width="150" MaxLength="15" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddTransLimit" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addTransLimit" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "TransLimit", "{0:##0.00}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TransLimit" Width="150" MaxLength="15" style="text-align:right" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TransLimit",  "{0:##0.00}") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransLimit" CssClass="NormalRed" runat="server" ControlToValidate="TransLimit" ErrorMessage="*" Display="dynamic" />                                        
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Limit in Words" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:TextBox ID="addLimitWord" Width="115" MaxLength="20" runat="Server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddLimitWord" CssClass="NormalRed"
                                            runat="server" ControlToValidate="addLimitWord" ErrorMessage="*" Display="dynamic">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "LimitWord")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="LimitWord" Width="115" style="text-align:right" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LimitWord") %>' MaxLength="20"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle CssClass="red" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right">             
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Approved")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="ChkApproved" Width="115"     runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"ApprovedBoolean") %>' ></asp:CheckBox>
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
            </div>
        </div>
    </div>
</asp:Content>
