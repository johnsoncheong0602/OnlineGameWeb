﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="GeneralSettings.aspx.vb" Inherits="Admin_GeneralSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="admincontent" runat="Server">
    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- Page Heading -->
        <h1 class="h3 mb-2 text-gray-800">Settings</h1>

        <!-- DataTales Example -->
        <div class="row">
            <div class="col-lg-6">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">General</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Company Name</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtCompany" runat="server" placeholder="Company Name" required="Required"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label>Copyright</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtCopyright" runat="server" placeholder="© 2020 Online Game Website" required="Required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Image</label>
                                <div class="custom-file">
                                    <asp:FileUpload CssClass="custom-file-input" ID="fileUploader" runat="server" ClientIDMode="Static" />
                                    <label class="custom-file-label" for="fileUploader" id="fileUploaderLabel" runat="server">Select logo file</label>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <asp:Image ID="imgLogo" runat="server" CssClass="img-fluid" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Recommend Bonus Percentage</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtRecBonusPercent" runat="server" placeholder="0.1" required="Required" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label>Recommend Bonus Minimum Amount</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtRecBonusMinAmount" runat="server" placeholder="10.0" required="Required" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group row">
                            <div class="col-sm-3 ml-auto">
                                <asp:Button class="btn btn-success btn-user btn-block" ID="btnSubmit" runat="server" Text="Submit" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Twilio SMS API</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Twilio Enable</label>
                                <asp:DropDownList CssClass="form-control form-control-user" ID="cmbTwilioEnabled" AutoPostBack="True" runat="server" placeholder="">
                                    <asp:ListItem Value="False">Disabled</asp:ListItem>
                                    <asp:ListItem Value="True">Enabled</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-6">
                                <label>Phone No.</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtTwilioPhone" runat="server" placeholder="Phone No." TextMode="Phone"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Account SID</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtTwilioSID" runat="server" placeholder="Account SID"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label>Auth Token</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtTwilioToken" runat="server" placeholder="Auth Token"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group row">
                            <div class="col-sm-3 ml-auto">
                                <asp:Button class="btn btn-success btn-user btn-block" ID="btnSubmitTwilio" runat="server" Text="Submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">HTML</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-sm-12 mb-3 mb-sm-0">
                                <asp:TextBox class="form-control form-control-user" ID="txtHTML" runat="server" placeholder="" TextMode="MultiLine" Height="350"></asp:TextBox> <%--Height="635"--%>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group row">
                            <div class="col-sm-3 ml-auto">
                                <asp:Button class="btn btn-success btn-user btn-block" ID="btnHTML" runat="server" Text="Submit" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Contacts</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-sm-12 mb-3 mb-sm-0">
                                <h6>Header & Footer Social Buttons</h6>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Facebook</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtFacebook" runat="server" placeholder="https://www.facebook.com" TextMode="Url"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label>Twitter</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtTwitter" runat="server" placeholder="https://www.twitter.com" TextMode="Url"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Instagram</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtInstagram" runat="server" placeholder="https://www.instagram.com" TextMode="Url"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label>TikTok</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtTikTok" runat="server" placeholder="https://www.tiktok.com" TextMode="Url"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>Youtube</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtYoutube" runat="server" placeholder="https://www.youtube.com/" TextMode="Url"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group row">
                            <div class="col-sm-12 mb-3 mb-sm-0">
                                <h6>Sub Header Contacts</h6>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>WhatsApp Number</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtWhatsapp" runat="server" placeholder="+6012-345 6789"></asp:TextBox>
                            </div>
                            <div class="col-sm-6">
                                <label>Telegram Username</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtTelegram" runat="server" placeholder="@username"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <label>WeChat ID</label>
                                <asp:TextBox class="form-control form-control-user" ID="txtWechat" runat="server" placeholder="wxid_012345678987"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group row">
                            <div class="col-sm-3 ml-auto">
                                <asp:Button class="btn btn-success btn-user btn-block" ID="btnSubmitSocial" runat="server" Text="Submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- End of Main Content -->

    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="js/sb-admin-2.min.js"></script>

    <!-- Page level plugins -->
    <script src="vendor/datatables/jquery.dataTables.min.js"></script>
    <script src="vendor/datatables/dataTables.bootstrap4.min.js"></script>

    <!-- Page level custom scripts -->
    <script src="js/demo/datatables-demo.js"></script>

    <script>
        $('#fileUploader').on('change', function () {
            //get the file name
            var fileName = $(this).val();
            //replace the "Choose a file" label
            $(this).next('.custom-file-label').html(fileName);
        })
    </script>
</asp:Content>

