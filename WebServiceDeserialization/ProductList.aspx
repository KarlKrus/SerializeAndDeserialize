﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="WebServiceDeserialization.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="_rptrEDP" runat="server" OnPreRender="_rptrEDP_PreRender">
        <ItemTemplate>
            <div class="row">
                <div class="col-sm-4"></div>
                <div class="col-sm-4">
                    <div class=" panel panel-info">
                        <div class="panel-body">
                            <asp:Image ID="img_Prod" runat="server" CssClass="img-responsive img-thumbnail center-block" AlternateText="Not Available" Width="100" Height="100" /></br>
                            <asp:HyperLink runat="server" CssClass="btn btn-hyperlink" Text='<%# Container.DataItem.ToString()%>' NavigateUrl='<%#string.Concat("~/ProductDetails.aspx?id=",Container.DataItem.ToString()) %>' ID="_hyprlnkEDP"></asp:HyperLink></br>
                        <%--<asp:Label ID="lbl_Store" runat="server" Text="Store:"></asp:Label></br>--%>
                            <asp:Label ID="lbl_Name" runat="server" Text="Name:"></asp:Label></br>
                            <asp:Label ID="lbl_Description" runat="server" Text="Description:"></asp:Label></br>
                            <asp:Label ID="lbl_Price" runat="server" Text="Price:"></asp:Label></br>
                            <asp:Label ID="lbl_Manufacturer" runat="server" Text="Manufacturer:"></asp:Label></br>
                            <asp:Label ID="lbl_Availability" runat="server" Text="Availability:"></asp:Label></br>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4"></div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
