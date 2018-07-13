<%@ Page Title="Kim Possible Fan World .:::. Kim Possible Screencaps" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Caps.aspx.cs" Inherits="kpfw.Caps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="interior cf">
        <div class="inner">
            <h1>Kim Possible Screen Caps</h1>
            <asp:Panel runat="server" CssClass="screen-caps" ID="pnlEpList">
                <p>Keep checking back as we continue to add more episodes! We currently have <asp:Literal runat="server" ID="ltlCapCount" /> screen captures of Kim Possible, Ron Stoppable, Rufus, Dr. Drakken, Shego, and the rest of the gang!</p>
                <p>Don't see your favorite cap? The system we use takes a screenshot once every second, so we know it misses some great shots now and then. Request those <a href="/Contact">here</a> and watch for them!</p>
                <br />
                <asp:Repeater runat="server" ID="rptSeason1" OnItemDataBound="rptSeason_ItemDataBound">
                    <HeaderTemplate>
                        <h2>Season 1</h2>
                        <div class="epsList">
                            <ol class="cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink runat="server" ID="hlk" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>
                </div>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="rptSeason2" OnItemDataBound="rptSeason_ItemDataBound">
                    <HeaderTemplate>
                        <h2>Season 2</h2>
                        <div class="epsList">
                            <ol class="lr cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink runat="server" ID="hlk" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>
                </div>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="rptSeason3" OnItemDataBound="rptSeason_ItemDataBound">
                    <HeaderTemplate>
                        <h2>Season 3</h2>
                        <div class="epsList">
                            <ol class="lr cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink runat="server" ID="hlk" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>
                </div>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="rptSeason4" OnItemDataBound="rptSeason_ItemDataBound">
                    <HeaderTemplate>
                        <h2>Season 4</h2>
                        <div class="epsList">
                            <ol class="lr cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink runat="server" ID="hlk" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>
                </div>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="rptMovies" OnItemDataBound="rptSeason_ItemDataBound">
                    <HeaderTemplate>
                        <h2>Movies</h2>
                        <div class="epsList">
                            <ol class="lr cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink runat="server" ID="hlk" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>
                </div>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="rptMisc" OnItemDataBound="rptSeason_ItemDataBound">
                    <HeaderTemplate>
                        <h2>Miscellaneous</h2>
                        <div class="epsList">
                            <ol class="lr cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                    <asp:HyperLink runat="server" ID="hlk" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ol>
                </div>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlThumbs" Visible="false">
                <h2 style="text-align: center;"><asp:Literal runat="server" ID="ltlEpTitle" /></h2>
                <p runat="server" id="pDescription"></p>
                <asp:Repeater runat="server" ID="rptMenu" OnItemDataBound="rptMenu_ItemDataBound">
                    <HeaderTemplate>
                        <div class="menu">
                            <ul class="lr cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <a href="#" runat="server" id="ancMenuItem">1-100</a>
                            <asp:Literal runat="server" ID="ltlMenuItem" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                </div>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Repeater runat="server" ID="rptThumbnails">
                    <HeaderTemplate>
                        <div id="thumbList" style="max-width: 900px; margin: 0 auto;">
                            <ul class="lr cf">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <a href="<%# "https:" + "//cdn.kpfanworld.com/" + ((string)Eval("Key")).Replace("/thumbs", "").Replace("_thumb", "") %>" class="image-popup" title="<%# EpTitle + ", https:" + "//cdn.kpfanworld.com/" + ((string)Eval("Key")).Replace("/thumbs", "").Replace("_thumb", "") %>">
                                <img src="<%# "https:" + "//cdn.kpfanworld.com/" + ((string)Eval("Key")) %>" />
                            </a>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                </div>
                    </FooterTemplate>
                </asp:Repeater>
                <!-- Add navigation here -->
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <asp:PlaceHolder runat="server" ID="plhCapsScript" Visible="false">
        <script>
            $(function () {
                $('.image-popup').magnificPopup({
                    type: 'image',
                    gallery: {
                        enabled: true,
                        navigateByImgClick: true,
                        preload: [1, 1] // Will preload 0 - before current, and 1 after the current image
                    },
                    image: {
                        tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
                        titleSrc: function (item) {
                            return item.el.attr('title') + '<small>by Kim Possible Fan World</small>';
                        }
                    },
                    callbacks: {
                        open: function () {
                            console.log(this.currItem.el[0].children[0].src);
                            dataLayer.push({ 'event': 'OpenModal', 'category': 'Caps', 'action': 'OpenModal', 'label': this.currItem.src, 'value' : this.currItem.el[0].children[0].src });
                            //ga('send', 'event', 'Caps', 'OpenModal', this.currItem.src, this.currItem.el[0].children[0].src);
                        },
                        change: function () {
                            console.log(this.currItem.el[0].children[0].src);
                            dataLayer.push({ 'event': 'ChangeModal', 'category': 'Caps', 'action': 'ChangeModal', 'label': this.currItem.src, 'value' : this.currItem.el[0].children[0].src });
                            //ga('send', 'event', 'Caps', 'ChangeModal', this.currItem.src, this.currItem.el[0].children[0].src);
                        }
                    }
                });
            });
        </script>
    </asp:PlaceHolder>
</asp:Content>