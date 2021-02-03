﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" EnableEventValidation="false" CodeFile="Transfer.aspx.vb" Inherits="Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="dvcontent" runat="Server">
    <div class="main">
        <section class="header-page inm_transfer">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <h1>TRANSFER</h1>
                        <ul class="breadcrumb">
                            <li><a href="Default.aspx">Home</a></li>
                            <li class="active">Transfer</li>
                        </ul>
                    </div>
                </div>
            </div>
        </section>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <article class="page-contact p-t-30 p-b-50">
                        <div class="row">
                            <div class="col-md-3">
                                <hr class="hr" />
                                <h4>Categories</h4>
                                <ul class="category m-b-60">
                                    <li><a href="Deposit.aspx">Deposit</a></li>
                                    <li><a href="Withdrawal.aspx">Withdrawal</a></li>
                                    <li class="active"><a href="Transfer.aspx">Transfer</a></li>
                                    <li><a href="TransactionHistory.aspx">Transaction History</a></li>
                                </ul>
                            </div>
                            <div class="col-md-9">
                                <hr class="hr" />
                                <h4>Transfer</h4>
                                <form action="#" class="m-t-30">
                                    <div class="form-group col-md-4">
                                        <label>From Product</label>
                                        <asp:DropDownList CssClass="form-control" ID="cmbProductFrom" AutoPostBack="False" runat="server" placeholder="">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>From Product</label>
                                        <asp:DropDownList CssClass="form-control" ID="cmbProductTo" AutoPostBack="False" runat="server" placeholder="">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>Amount</label>
                                        <asp:UpdatePanel ID="upForm" runat="server">
                                            <ContentTemplate>
                                                <asp:ScriptManager ID="smForm" runat="server"></asp:ScriptManager>
                                                <asp:TextBox CssClass="form-control" ID="txtAmount" runat="server" placeholder="30.00" TextMode="Number" required="Required" AutoCompleteType="None" AutoPostBack="True"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Captcha</label>
                                        <asp:TextBox class="form-control" ID="txtVerification" runat="server" placeholder="" TextMode="SingleLine" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label></label>
                                        <asp:UpdatePanel ID="upCaptcha" runat="server">
                                                <ContentTemplate>
                                                    <a href="#" id="refreshCaptcha" runat="server">
                                                        <img src="#" id="captchaImg" runat="server" class="img-responsive" alt="" style="height: 50px;" /></a>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group m-t-30 col-md-12">
                                        <asp:Button class="btn btn-palovit center-block" ID="btnSubmit" runat="server" Text="SUBMIT" />
                                    </div>
                                </form>
                            </div>
                        </div>
                    </article>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
