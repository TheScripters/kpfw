using Markdig;
using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class Guide : System.Web.UI.Page
    {
        protected MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseCaps().Build();
        private bool IsLoggedIn { get { return Page.User.Identity.IsAuthenticated; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            string epUrl = Page.RouteData.Values["Episode"]?.ToString();
            string epTitle = Page.RouteData.Values["EpisodeTranscript"]?.ToString();
            if (epUrl != null)
            {
                if (new Regex("^[0-9]+$").IsMatch(epUrl))
                {
                    epUrl = Episode.GetUrl(Convert.ToInt32(epUrl));
                    if (epUrl == "")
                    {
                        Response.Redirect("~/Guides");
                        return;
                    }
                    Response.RedirectPermanent($"~/Guides/{epUrl}");
                    return;
                }

                Episode ep = Episode.GetByUrl(epUrl);
                if (ep == null)
                {
                    Response.Redirect("~/Guides");
                    return;
                }
                LoadEpisode(ep);
            }
            else if (epTitle != null)
            {
                if (new Regex("^[0-9]+$").IsMatch(epTitle.Trim()))
                {
                    epTitle = Episode.GetUrl(Convert.ToInt32(epTitle));
                    if (epTitle == "")
                    {
                        Response.Redirect("~/Guides");
                        return;
                    }
                    Response.RedirectPermanent($"~/Guides/{epTitle}/Transcript");
                    return;
                }

                Episode ep = Episode.GetByUrl(epTitle);
                if (ep == null)
                {
                    Response.Redirect("~/Guides");
                    return;
                }

                LoadTranscript(ep);
            }
            else
            {
                List<Episode> l = Episode.List();
                var s = l.Where(ep => ep.Season == 1).ToList();
                rptSeries.DataSource = l.Select(i => i.Season).Distinct();
                rptSeries.DataBind();
                //rptS1.DataSource = s;
                //rptS1.DataBind();

                //s = l.Where(ep => ep.Season == 2).ToList();
                //rptS2.DataSource = s;
                //rptS2.DataBind();

                //s = l.Where(ep => ep.Season == 3).ToList();
                //rptS3.DataSource = s;
                //rptS3.DataBind();

                //s = l.Where(ep => ep.Season == 4).ToList();
                //rptS4.DataSource = s;
                //rptS4.DataBind();
            }

            pnlList.Visible = epUrl == null && epTitle == null;
            pnlEpisode.Visible = epUrl != null;
            pnlTranscript.Visible = epTitle != null;
        }

        protected void LoadEpisode(Episode ep)
        {
            LoadCapsLink(ep, hlkCapsLink, plhCapsLink);

            hlkTranscriptLink.Visible = !String.IsNullOrWhiteSpace(ep.Transcript);
            hlkTranscriptLink.NavigateUrl = $"/Guides/{ep.UrlLabel}/Transcript";
            hlkTranscriptLink.Text = $"{ep.Title} Transcript";
            Title = $"Kim Possible Fan World .:::. {ep.Title} Episode Guide";
            MetaDescription = ep.Description;
            hEpisodeTitle.InnerText = ep.Title;
            ltlDescription.Text = ep.Description;

            ltlDirector.Text = ep.Director.RenderImdbLinks();
            ltlWriter.Text = ep.Writer.RenderImdbLinks();
            ltlProducer.Text = ep.Producer.RenderImdbLinks();
            ltlProdNumber.Text = ep.ProductionNumber;
            ltlAirDate.Text = String.Format("{0:MMMM d, yyyy}", ep.AirDate);
            ltlExecProducer.Text = ep.ExecutiveProducer.RenderImdbLinks();
            ltlSeason.Text = ep.Season.ToString();
            ltlStudio.Text = Markdown.ToHtml(ep.Studio, pipeline).Nl2Br();

            plhEditBasic.Visible = IsLoggedIn;

            ltlStars.Text = ep.Stars.RenderImdbLinks();
            ltlGuestStars.Text = ep.GuestStars.RenderImdbLinks();

            plhEditCast.Visible = IsLoggedIn;

            var notes = Episode.ListNotes(ep.Id);
            pNoNotes.Visible = notes.Rows.Count == 0;
            rptNotes.DataSource = notes;
            rptNotes.DataBind();

            var quotes = Episode.ListQuotes(ep.Id);
            pNoQuotes.Visible = quotes.Rows.Count == 0;
            rptQuotes.DataSource = quotes;
            rptQuotes.DataBind();

            var goofs = Episode.ListGoofs(ep.Id);
            pNoGoofs.Visible = goofs.Rows.Count == 0;
            rptGoofs.DataSource = goofs;
            rptGoofs.DataBind();

            var cultural = Episode.ListCultural(ep.Id);
            pNoCultural.Visible = cultural.Rows.Count == 0;
            rptCultural.DataSource = cultural;
            rptCultural.DataBind();
        }

        protected void LoadTranscript(Episode ep)
        {
            hlkBack.NavigateUrl = $"/Guides/{ep.UrlLabel}";
            hlkBack.Text = $"&laquo; Back to {ep.Title} Episode Guide";

            LoadCapsLink(ep, hlkTransciptCapsLink, null);

            Title = $"Kim Possible Fan World .:::. {ep.Title} Episode Transcript";
            hTranscriptTitle.InnerText = ep.Title + " Transcript";
            ltlTranscript.Text = Markdown.ToHtml(ep.Transcript.Nl2Br(), pipeline);
        }

        private void LoadCapsLink(Episode ep, HyperLink hlk, PlaceHolder plh)
        {
            if (ep.Season == 1)
            {
                var caps = EpCapsList.S1List.Where(e => e.UrlLabel == ep.UrlLabel);
                if (caps.Count() > 0)
                {
                    EpisodeTitle cap = caps.Single();
                    if (!String.IsNullOrWhiteSpace(cap.CDNPath))
                    {
                        if (plh != null)
                            plh.Visible = true;
                        hlk.NavigateUrl = $"/Caps/{ep.UrlLabel}";
                        hlk.Text = $"{ep.Title} Screencaps";
                    }
                }
            }
            if (ep.Season == 2)
            {
                var caps = EpCapsList.S2List.Where(e => e.UrlLabel == ep.UrlLabel);
                if (caps.Count() > 0)
                {
                    EpisodeTitle cap = caps.Single();
                    if (!String.IsNullOrWhiteSpace(cap.CDNPath))
                    {
                        if (plh != null)
                            plh.Visible = true;
                        hlk.NavigateUrl = $"/Caps/{ep.UrlLabel}";
                        hlk.Text = $"{ep.Title} Screencaps";
                    }
                }
            }
            if (ep.Season == 3)
            {
                var caps = EpCapsList.S3List.Where(e => e.UrlLabel == ep.UrlLabel);
                if (caps.Count() > 0)
                {
                    EpisodeTitle cap = caps.Single();
                    if (!String.IsNullOrWhiteSpace(cap.CDNPath))
                    {
                        if (plh != null)
                            plh.Visible = true;
                        hlk.NavigateUrl = $"/Caps/{ep.UrlLabel}";
                        hlk.Text = $"{ep.Title} Screencaps";
                    }
                }
            }
            if (ep.Season == 4)
            {
                var caps = EpCapsList.S4List.Where(e => e.UrlLabel == ep.UrlLabel);
                if (caps.Count() > 0)
                {
                    EpisodeTitle cap = caps.Single();
                    if (!String.IsNullOrWhiteSpace(cap.CDNPath))
                    {
                        if (plh != null)
                            plh.Visible = true;
                        hlk.NavigateUrl = $"/Caps/{ep.UrlLabel}";
                        hlk.Text = $"{ep.Title} Screencaps";
                    }
                }
            }
        }

        protected void rptSeries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater r = e.Item.FindControl("rptSeason") as Repeater;

                r.DataSource = Episode.List((int)e.Item.DataItem);
                r.DataBind();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}