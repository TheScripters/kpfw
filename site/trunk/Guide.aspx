<%@ Page Title="Kim Possible Fan World .:::. Guides" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Guide.aspx.cs" Inherits="kpfw.Guide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="interior cf">
        <div class="inner">
            <asp:Panel runat="server" ID="pnlList">
                <h1>Guides</h1>
                <asp:Repeater runat="server" ID="rptSeries" OnItemDataBound="rptSeries_ItemDataBound">
                    <ItemTemplate>
                        <asp:Repeater runat="server" ID="rptSeason">
                            <HeaderTemplate>
                                <h2>Season <%# ((RepeaterItem)Container.Parent.Parent).DataItem.ToString() %></h2>
                                <p class="hint">
                                    <img src="/Images/swipe-icon.png" /><span>Swipe/scroll to view table.</span>
                                </p>
                                <div class="chart">
                                    <div class="chart-wrap">
                                        <table>
                                            <tr class="head">
                                                <th>Ep.</th>
                                                <th>Ep. Title</th>
                                                <th>Air Date</th>
                                                <th>Prod. #</th>
                                            </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Number") %></td>
                                    <td><a href="/Guides/<%# Eval("UrlLabel") %>"><%# Eval("Title") %></a></td>
                                    <td><%# String.Format("{0:MMMM dd, yyyy}", Eval("AirDate")) %></td>
                                    <td><%# Eval("ProductionNumber") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                                </div>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="rptMovies">
                    <HeaderTemplate>
                        <h2>Movies/DVDs</h2>
                        <p class="hint">
                            <img src="/Images/swipe-icon.png" /><span>Swipe/scroll to view table.</span>
                        </p>
                        <div class="chart">
                            <div class="chart-wrap">
                                <table>
                                    <tr class="head">
                                        <th>Ep.</th>
                                        <th>Ep. Title</th>
                                        <th>Air Date</th>
                                        <th>Prod. #</th>
                                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Number") %></td>
                            <td><a href="/Guides/<%# Eval("UrlLabel") %>"><%# Eval("Title") %></a></td>
                            <td><%# String.Format("{0:MMMM dd, yyyy}", Eval("AirDate")) %></td>
                            <td><%# Eval("ProductionNumber") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                        </div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlEpisode">
                <h1 runat="server" id="hEpisodeTitle"></h1>
                <h2>Basic Information</h2>
                <p>
                    <strong>Description:</strong>
                    <asp:Literal runat="server" ID="ltlDescription" />
                </p>
                <p>
                    <strong>Director:</strong>
                    <asp:Literal runat="server" ID="ltlDirector" />
                </p>
                <p>
                    <strong>Writer:</strong>
                    <asp:Literal runat="server" ID="ltlWriter" />
                </p>
                <p>
                    <strong>Producer:</strong>
                    <asp:Literal runat="server" ID="ltlProducer" />
                </p>
                <p>
                    <strong>Studio: </strong>
                    <asp:Literal runat="server" ID="ltlStudio" />
                </p>
                <p>
                    <strong>Executive Producer:</strong>
                    <asp:Literal runat="server" ID="ltlExecProducer" />
                </p>
                <p>
                    <strong>Air Date:</strong>
                    <asp:Literal runat="server" ID="ltlAirDate" />
                </p>
                <p>
                    <strong>Season:</strong> Season
                        <asp:Literal runat="server" ID="ltlSeason" />
                </p>
                <p>
                    <strong>Production Number:</strong>
                    <asp:Literal runat="server" ID="ltlProdNumber" />
                </p>
                <p>
                    <asp:PlaceHolder runat="server" ID="plhCapsLink" Visible="false">
                        <asp:HyperLink runat="server" ID="hlkTranscriptLink" />&nbsp;|&nbsp;<asp:HyperLink runat="server" ID="hlkCapsLink" />
                    </asp:PlaceHolder>
                </p>
                <asp:PlaceHolder runat="server" ID="plhEditBasic">
                    <p>
                        <asp:Button runat="server" ID="btnEdit" Text="Edit Basic Information" OnClick="btnEdit_Click" CommandArgument="Basic" />
                    </p>
                </asp:PlaceHolder>
                <h2>Cast</h2>
                <p>
                    <strong>Regular Cast:</strong>
                    <asp:Literal runat="server" ID="ltlStars" />
                </p>
                <p>
                    <strong>Guest Stars:</strong>
                    <asp:Literal runat="server" ID="ltlGuestStars" />
                </p>
                <asp:PlaceHolder runat="server" ID="plhEditCast">
                    <p>
                        <asp:Button runat="server" Text="Edit Cast" OnClick="btnEdit_Click" CommandArgument="Cast" />
                    </p>
                </asp:PlaceHolder>
                <h2>Notes</h2>
                <p runat="server" id="pNoNotes" style="font-style: italic">Sorry, nothing has been provided for this episode yet. <a href="/Contact">Contact us</a> for suggestions!</p>
                <asp:Repeater runat="server" ID="rptNotes">
                    <ItemTemplate>
                        <p>
                            <%# Markdig.Markdown.ToHtml(Robo.Extensions.Nl2Br((string)Eval("Note")), pipeline) %>
                        </p>
                        <asp:PlaceHolder runat="server" ID="plhEditCast">
                            <p>
                                <asp:Button runat="server" ID="Button1" Text="Edit Cast" OnClick="btnEdit_Click" />
                            </p>
                        </asp:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater>
                <h2>Quotes</h2>
                <p runat="server" id="pNoQuotes" style="font-style: italic">Sorry, nothing has been provided for this episode yet. <a href="/Contact">Contact us</a> for suggestions!</p>
                <asp:Repeater runat="server" ID="rptQuotes">
                    <ItemTemplate>
                        <p>
                            <%# Markdig.Markdown.ToHtml(Robo.Extensions.Nl2Br((string)Eval("QuoteText")), pipeline) %>
                        </p>
                        <asp:PlaceHolder runat="server" ID="plhEditCast">
                            <p>
                                <asp:Button runat="server" ID="Button1" Text="Edit Cast" OnClick="btnEdit_Click" />
                            </p>
                        </asp:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater>
                <h2>Goofs</h2>
                <p runat="server" id="pNoGoofs" style="font-style: italic">Sorry, nothing has been provided for this episode yet. <a href="/Contact">Contact us</a> for suggestions!</p>
                <asp:Repeater runat="server" ID="rptGoofs">
                    <ItemTemplate>
                        <p>
                            <%# Markdig.Markdown.ToHtml(Robo.Extensions.Nl2Br((string)Eval("GoofText")), pipeline) %>
                        </p>
                    </ItemTemplate>
                </asp:Repeater>
                <h2>Cultural References</h2>
                <p runat="server" id="pNoCultural" style="font-style: italic">Sorry, nothing has been provided for this episode yet. <a href="/Contact">Contact us</a> for suggestions!</p>
                <asp:Repeater runat="server" ID="rptCultural">
                    <ItemTemplate>
                        <p>
                            <%# Markdig.Markdown.ToHtml(Robo.Extensions.Nl2Br((string)Eval("CulturalText")), pipeline) %>
                        </p>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="pnlTranscript" runat="server">
                <h1 runat="server" id="hTranscriptTitle"></h1>
                <asp:HyperLink runat="server" ID="hlkBack" />&nbsp;|&nbsp;<asp:HyperLink runat="server" ID="hlkTransciptCapsLink" />
                <asp:Literal runat="server" ID="ltlTranscript" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>
