using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class Caps : System.Web.UI.Page
    {
        private static string[] d3 = { "the-secret-files-wacko-bad-guys", "could-it-be", "get-your-shine-on", "its-just-you", "say-the-word", "the-naked-mole-rap",
            "so-the-drama-deleted-scenes", "kim-possible-tv-ad", "the-villain-files-opening", "the-villain-files-villain-party", "rufus-vs-commodore-puddles", "day-of-the-snowmen" };
        protected string EpTitle { get { return ViewState["EpTitle"] as string ?? ""; } set { ViewState["EpTitle"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RawUrl.EndsWith("/"))
            {
                Response.RedirectPermanent("/" + Request.RawUrl.Trim('/'));
                return;
            }
            if (!IsPostBack)
            {
                if (Page.RouteData.Values["Episode"] == null)
                {
                    pnlEpList.Visible = true;
                    pnlThumbs.Visible = false;
                    rptSeason1.DataSource = EpCapsList.S1List;
                    rptSeason1.DataBind();
                    rptSeason2.DataSource = EpCapsList.S2List;
                    rptSeason2.DataBind();
                    rptSeason3.DataSource = EpCapsList.S3List;
                    rptSeason3.DataBind();
                    rptSeason4.DataSource = EpCapsList.S4List;
                    rptSeason4.DataBind();
                    rptMovies.DataSource = EpCapsList.MovieList;
                    rptMovies.DataBind();
                    rptMisc.DataSource = EpCapsList.MiscList;
                    rptMisc.DataBind();

                    int capCount = 0;
                    foreach (var l in EpCapsList.S1List)
                        capCount += l.Count;
                    foreach (var l in EpCapsList.S2List)
                        capCount += l.Count;
                    foreach (var l in EpCapsList.S3List)
                        capCount += l.Count;
                    foreach (var l in EpCapsList.S4List)
                        capCount += l.Count;
                    foreach (var l in EpCapsList.MovieList)
                        capCount += l.Count;
                    foreach (var l in EpCapsList.MiscList)
                        capCount += l.Count;

                    ltlCapCount.Text = capCount.ToString("N0");
                    MetaDescription = $"We have {capCount:N0} Kim Possible screen shots, caps, memes, etc";
                }
                else
                {
                    pnlEpList.Visible = false;
                    pnlThumbs.Visible = true;
                    plhCapsScript.Visible = true;
                    bool isValid = false;
                    string episodeUrl = Page.RouteData.Values["Episode"].ToString();
                    string epUrl = episodeUrl;
                    if (episodeUrl.ToLower().Contains("_") && !episodeUrl.ToLower().Contains("veiwpic"))
                    {
                        episodeUrl = episodeUrl.ToLower().Replace(".php", "").Replace("_", "-");
                    }
                    EpisodeTitle ep = EpCapsList.S1List.Where(p => p.UrlLabel == episodeUrl).SingleOrDefault();
                    if (ep != null && !String.IsNullOrWhiteSpace(ep.Title))
                        isValid = true;
                    if (ep == null)
                    {
                        ep = EpCapsList.S2List.Where(p => p.UrlLabel == episodeUrl).SingleOrDefault();
                        if (!String.IsNullOrWhiteSpace(ep?.Title))
                            isValid = true;
                    }
                    if (ep == null)
                    {
                        ep = EpCapsList.S3List.Where(p => p.UrlLabel == episodeUrl).SingleOrDefault();
                        if (!String.IsNullOrWhiteSpace(ep?.Title))
                            isValid = true;
                    }
                    if (ep == null)
                    {
                        ep = EpCapsList.S4List.Where(p => p.UrlLabel == episodeUrl).SingleOrDefault();
                        if (!String.IsNullOrWhiteSpace(ep?.Title))
                            isValid = true;
                    }
                    if (ep == null)
                    {
                        ep = EpCapsList.MovieList.Where(p => p.UrlLabel == episodeUrl).SingleOrDefault();
                        if (!String.IsNullOrWhiteSpace(ep?.Title))
                            isValid = true;
                    }
                    if (ep == null)
                    {
                        ep = EpCapsList.MiscList.Where(p => p.UrlLabel == episodeUrl).SingleOrDefault();
                        if (!String.IsNullOrWhiteSpace(ep?.Title))
                            isValid = true;
                    }
                    if (RedirectTable.CapsList.ContainsKey(epUrl))
                    {
                        if (RedirectTable.CapsList[epUrl].StartsWith("/"))
                            Response.RedirectPermanent(RedirectTable.CapsList[epUrl]);
                        else
                            Response.RedirectPermanent($"/Caps/{RedirectTable.CapsList[epUrl]}");
                        return;
                    }
                    if (!isValid)
                    {
                        using (FileStream fs = new FileStream(Server.MapPath("~/App_Data/Urls.txt"), FileMode.Append))
                        {
                            byte[] url = System.Text.Encoding.UTF8.GetBytes(Request.RawUrl + "\r\n");
                            fs.Write(url, 0, url.Length);
                        }
                        Response.Redirect("/Caps");
                        return;
                    }
                    if (isValid && epUrl.ToLower().Contains("_"))
                    {
                        Response.RedirectPermanent("/Caps/" + episodeUrl);
                        return;
                    }
                    ltlEpTitle.Text = ep.Title;
                    EpTitle = ep.Title;
                    Title = $"{ep.Title} Screen Captures | Kim Possible Fan World";
                    Episode epDetails = Episode.GetByUrl(ep.UrlLabel);
                    MetaDescription = epDetails?.Description;
                    pDescription.InnerText = epDetails?.Description;
                    if (epDetails != null)
                    {
                        pDescription.InnerHtml += " <a href=\"/Contact\">Contact Us</a> to suggest edits and corrections!";
                    }
                    List<string> MenuItems = new List<string>();
                    IEnumerable<S3Object> caps = new List<S3Object>();
                    if (ep.Count == -1)
                    {
                        BasicAWSCredentials cred = new BasicAWSCredentials("", "");
                        AmazonS3Client c = new AmazonS3Client(cred, Amazon.RegionEndpoint.USWest2);
                        List<S3Object> objs = new List<S3Object>();
                        var items = c.ListObjectsV2(new ListObjectsV2Request() { BucketName = "kpfw", Prefix = $"caps/{ep.CDNPath}/thumbs/" });
                        //var items = c.ListObjectsV2(new ListObjectsV2Request() { BucketName = "kpfw", Prefix = "caps/", Delimiter = "/" });
                        //List<string> folders = items.CommonPrefixes;
                        //bltItems.DataSource = folders;
                        //bltItems.DataBind();
                        string contToken = items.NextContinuationToken;
                        foreach (var s3 in items.S3Objects)
                        {
                            if (s3.Key == items.Prefix)
                                continue;
                            objs.Add(s3);
                        }

                        while (items.S3Objects.Count > 0 && contToken != null)
                        {
                            items = c.ListObjectsV2(new ListObjectsV2Request() { BucketName = "kpfw", Prefix = $"caps/{ep.CDNPath}/thumbs/", ContinuationToken = contToken });
                            contToken = items.NextContinuationToken;
                            if (items.S3Objects.Count == 0)
                                break;

                            foreach (var s3 in items.S3Objects)
                            {
                                objs.Add(s3);
                            }
                        }
                        caps = objs.OrderBy(s => s.Key);
                        try
                        {
                            caps = objs.OrderBy(s => Convert.ToInt32(s.Key.Replace($"caps/{ep.CDNPath}/thumbs/Image", "").Replace("_thumb.jpg", ""))).ToList();
                        }
                        catch { }
                        MenuItems.Add($"/Caps/{ep.UrlLabel}");
                        int item = 100;
                        ViewState["maxNum"] = caps.Count();
                        while (item < caps.Count())
                        {
                            MenuItems.Add($"/Caps/{ep.UrlLabel}/{item}");
                            item += 100;
                        }
                    }
                    else
                    {
                        int ic = 0;
                        string fmt = "D4";
                        if (d3.Contains(ep.UrlLabel))
                            fmt = "D3";
                        if (ep.UrlLabel == "so-the-drama")
                            fmt = "D";
                        while (ic < ep.Count)
                        {
                            ((List<S3Object>)caps).Add(new S3Object { BucketName = "kpfw", Key = $"caps/{ep.CDNPath}/thumbs/Image" + String.Format("{0:" + fmt + "}", ic + 1) + "_thumb.jpg" });
                            ic++;
                        }
                        MenuItems.Add($"/Caps/{ep.UrlLabel}");
                        int item = 100;
                        ViewState["maxNum"] = ep.Count;
                        while (item < ep.Count)
                        {
                            MenuItems.Add($"/Caps/{ep.UrlLabel}/{item}");
                            item += 100;
                        }
                    }
                    rptMenu.DataSource = MenuItems;
                    rptMenu.DataBind();
                    int startNum = 0;
                    if (Page.RouteData.Values["StartNum"] != null)
                    {
                        startNum = Convert.ToInt32(Page.RouteData.Values["StartNum"]);
                        int diff = startNum % 100;
                        if (diff != 0)
                        {
                            startNum -= diff;
                            Response.Redirect($"/Caps/{ep.UrlLabel}/{startNum}");
                            return;
                        }
                    }
                    caps = caps.Skip(startNum);
                    if (caps.Count() > 100)
                        caps = caps.Take(100);

                    rptThumbnails.DataSource = caps;
                    rptThumbnails.DataBind();
                    //bltItems.DataSource = caps;
                    //bltItems.DataTextField = "key";
                    //bltItems.DataBind();
                }
            }
        }

        protected void rptSeason_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EpisodeTitle i = (EpisodeTitle)e.Item.DataItem;
                HyperLink hlk = e.Item.FindControl("hlk") as HyperLink;

                hlk.Text = i.Title;
                hlk.NavigateUrl = "/Caps/" + i.UrlLabel;
                hlk.Enabled = i.CDNPath != "";
            }
        }

        protected void rptThumbnails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                S3Object i = (S3Object)e.Item.DataItem;
                HyperLink img = e.Item.FindControl("imgThumbnail") as HyperLink;

                img.ImageUrl = "https:" + "//cdn.kpfanworld.com/" + i.Key;
                img.NavigateUrl = "https:" + "//cdn.kpfanworld.com/" + i.Key.Replace("/thumbs", "").Replace("_thumb", "");
            }
        }

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string item = (string)e.Item.DataItem;
                HtmlAnchor anc = e.Item.FindControl("ancMenuItem") as HtmlAnchor;
                Literal ltl = e.Item.FindControl("ltlMenuItem") as Literal;

                int curr = 1;
                if (Page.RouteData.Values["StartNum"] != null)
                {
                    curr = Convert.ToInt32(Page.RouteData.Values["StartNum"]);
                }
                anc.HRef = item;
                string[] items = item.Trim('/').Split('/');
                if (items.Length == 2 || items[2] == "")
                {
                    anc.Visible = curr > 1;
                    ltl.Visible = curr == 1;
                    anc.InnerText = ltl.Text = "1-100";
                }
                else
                {
                    int start = Int32.Parse(items[2]);
                    int max = (int)ViewState["maxNum"];
                    int end = start + 100;
                    if (end > max)
                        end = max;
                    anc.InnerText = ltl.Text = start + "-" + end;
                    anc.Visible = curr != start;
                    ltl.Visible = curr == start;
                }
            }
        }
    }
}