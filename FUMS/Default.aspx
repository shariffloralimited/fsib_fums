<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" %>
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
            <!--progress bar start-->
            <section class="panel">
                <header class="panel-heading">
                    Select operation           
                    <span class="tools pull-right">
                        <a href="javascript:;" class="fa fa-chevron-down"></a>
                    </span>
                </header>
                <div class="panel-body">
                    <div class="row">                        
                        <div class="col-md-3">
                            <div class="module-name-work">
                                <a href="Users.aspx">
                                    <div class="text-center" style="border: solid 1px #EFEFEF; padding:20px">
                                        <span style="font-size: 100px" class="glyphicon glyphicon-th-list" aria-hidden="true"></span>
                                        <div>
                                            <span style="line-height: 30px; font-size: 18px">Manage Users</span>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="col-md-3" id="createUser">
                            <div class="module-name-work">
                                <a href="InsertUser.aspx">
                                    <div class="text-center" style="border: solid 1px #EFEFEF; padding:20px">
                                        <span style="font-size: 100px" class="glyphicon glyphicon-user" aria-hidden="true"></span>
                                        <div>
                                            <span style="line-height: 30px; font-size: 18px">Create New User</span>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>                       
                        <div  class="col-md-3">
                            <div class="module-name-work">
                                <a href="TransactionLimit.aspx">
                                    <div class="text-center" style="border: solid 1px #EFEFEF; padding:20px">
                                        <span style="font-size: 100px" class="glyphicon glyphicon-stats" aria-hidden="true"></span>
                                        <div >
                                            <span style="line-height: 30px; font-size: 18px">Manage Trans Limit</span>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <a href="RolewiseUsers.aspx">
                            <div class="module-name-work">
                                    <div class="text-center" style="border: solid 1px #EFEFEF; padding:20px">
                                        <span style="font-size: 100px" class="glyphicon glyphicon-indent-right" aria-hidden="true"></span>
                                        <div>
                                            <span style="line-height: 30px; font-size: 18px">Rolewise Report</span>
                                        </div>
                                    </div>
                                </a>
                            </div>                            
                           
                        </div>
                    <div class="row" >                   
                             <div class="col-md-offset-3 col-md-3">
                            <a href="StatuswiseUsers.aspx">
                            <div class="module-name-work">
                                    <div class="text-center" style="border: solid 1px #EFEFEF; padding:20px">
                                        <span style="font-size: 100px" class="glyphicon glyphicon-edit" aria-hidden="true"></span>
                                        <div>
                                            <span style="line-height: 30px; font-size: 18px">Statuswise Report</span>
                                        </div>
                                    </div>
                                     </div>
                                </a>
                               
                            </div>
                            <div class="col-md-3">
                            <a href="AuditLog.aspx">
                            <div class="module-name-work">
                                    <div class="text-center" style="border: solid 1px #EFEFEF; padding:20px">
                                        <span style="font-size: 100px" class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>
                                        <div>
                                            <span style="line-height: 30px; font-size: 18px">Audit Log</span>
                                        </div>
                                    </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                </div>
            </section>
        </div>
    </div>
    <!--progress bar end-->
</asp:Content>
