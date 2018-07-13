<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="kpfw.ArticlePage" %>
<%@ Register Src="~/Controls/ListPager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Namespace="MirrorControl" Assembly="MirrorControl" TagPrefix="mc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <asp:Panel runat="server" ID="pnlList">
        <uc:Pager runat="server" PageSize="25" ID="pager" OnPageNumberChange="pager_PageNumberChange" />
        <asp:Repeater runat="server" ID="rptArticleList">
            <ItemTemplate>

            </ItemTemplate>
        </asp:Repeater>
        <mc:Mirror runat="server" ControlID="pager" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlArticle">

    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" Runat="Server">
</asp:Content>