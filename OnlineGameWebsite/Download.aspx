﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" EnableEventValidation="false" CodeFile="Download.aspx.vb" Inherits="Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="dvcontent" runat="Server">
    <div class="main">
        <section class="header-page">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <h1><%=Resources.Resource.DownloadMenu %></h1>
                        <ul class="breadcrumb">
                            <li><a href="Default.aspx"><%=Resources.Resource.Home %></a></li>
                            <li class="active"><%=Resources.Resource.Download %></li>
                        </ul>
                    </div>
                </div>
            </div>
        </section>
        <section class="form2">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 palovit-title m-b-30">
                        <hr class="hr" />
                        <h2><%=Resources.Resource.DownloadOurApp %></h2>
                        <p><%=Resources.Resource.DownloadOurAppSubtitle %></p>
                    </div>
                    <div class="col-md-8 col-md-offset-2">
                        <div>
                            <img src="Theme/img/iphone12s.png" class="img-responsive" alt="" />
                            <div class="col-md-6">
                                <button type="button" onclick="window.location.href='#'" class="btn btn-slider-black center-block"><%=Resources.Resource.DownloadMenu %><i class="fa fa-android"></i></button>
                            </div>
                            <div class="col-md-6">
                                <button type="button" onclick="window.location.href='#'" class="btn btn-slider-black center-block"><%=Resources.Resource.DownloadMenu %><i class="fa fa-apple"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

