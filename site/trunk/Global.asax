<%@ Application Language="C#" %>
<%@ Import Namespace="kpfw" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext ctx = HttpContext.Current;
        string url = ctx.Request.RawUrl.Trim('/');
        if (url.Contains("%E2%80%8B"))
        {
            Response.RedirectPermanent(url.Replace("%E2%80%8B", ""));
            return;
        }
        if (url.ToLower() == "eplist")
        {
            ctx.Response.RedirectPermanent("/Guides", true);
            return;
        }
        if (url.ToLower().StartsWith("transcript"))
        {
            if (url.ToLower() == "transcript")
            {
                ctx.Response.RedirectPermanent("/Guides", true);
                return;
            }
            string newUrl = $"/Guides/{StripKpfw(url.ToLower().Replace("transcript/", ""))}/Transcript";
            ctx.Response.RedirectPermanent(newUrl, true);
            return;
        }
        if (url.ToLower().Contains("chat.php") || url.ToLower().Contains("chat/chat.php"))
        {
            ctx.Response.RedirectPermanent("/Chat");
            return;
        }
        if (url == "giveaway")
        {
            ctx.Response.Redirect("https://giveaway.amazon.com/p/db4e15630ca7f989");
            return;
        }
    }
    void Application_Start(object sender, EventArgs e)
    {
        RegisterRoutes(RouteTable.Routes);
        RegisterBundles(BundleTable.Bundles);
    }
    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        // Get the exception object.
        Exception exc = Server.GetLastError();

        if (exc is HttpUnhandledException)
        {
            if (exc.InnerException == null)
            {
                Server.Transfer("/500.html", false);
                return;
            }
            exc = exc.InnerException;
        }
        // Handle HTTP errors
        if (exc is HttpException)
        {
            if (((HttpException)exc).GetHttpCode() == 404)
            {
                // Log if wished.
                Server.ClearError();
                Server.Transfer("/404.html", false);
                return;
            }
        }
        Server.Transfer("/500.html", false);

        string subjectAdd = "";

        if (Robo.ExcDetails.IsConnectivityWebException(exc))
            subjectAdd = "connection";

        if (!Robo.ExcDetails.IsInfrastructureWebException(exc) && !(exc is HttpException && ((HttpException)exc).GetHttpCode() == 404))
            Notification.SendError(exc, subjectAdd);

        Server.ClearError();
        //Server.Transfer(redirectUrl, false);
    }
    void RegisterBundles(BundleCollection bundles)
    {
        bundles.Add(new ScriptBundle("~/bundle/js").Include("~/js/modernizr.js", "~/js/jquery.magnific-popup.min.js", "~/js/double-tap.js", "~/js/mobile-menu.js", "~/js/webfontloader/webfontloader.js", "~/js/load-universal.js", "~/js/footer/sticky-footer.min.js", "~/js/noty/noty.min.js", "~/js/CustomLock.js"));
        bundles.Add(new StyleBundle("~/bundle/css").Include("~/css/reset.css", "~/css/fa/font-awesome.min.css", "~/css/forms.css", "~/css/Site.css", "~/css/magnific-popup.css", "~/css/noty/noty.css", "~/css/noty/relax.css"));
    }
    void RegisterRoutes(RouteCollection routes)
    {
        routes.Ignore("{*allaxd}", new { allaxd = @".*\.axd(/.*)?" });
        routes.Ignore("{*allasmx}", new { allasmx = @".*\.asmx(/.*)?" });
        routes.Ignore("{*allxml}", new { allxml = @".*\.xml(/.*)?" });
        routes.Ignore("{*alljs}", new { alljs = @".*\.js(/.*)?" });
        routes.Ignore("{*allcss}", new { allcss = @".*\.css(/.*)?" });
        routes.Ignore("{*alljpg}", new { alljpg = @".*\.jpg(/.*)?" });
        routes.Ignore("{*allpng}", new { allpng = @".*\.png(/.*)?" });
        routes.Ignore("{*allgif}", new { allgif = @".*\.gif(/.*)?" });
        routes.Ignore("{*alltif}", new { alltif = @".*\.tif(/.*)?" });
        routes.Ignore("{*allhtml}", new { allhtml = @".*\.html(/.*)?" });
        routes.Ignore("{*allhtml}", new { allhtml = @".*\.ico(/.*)?" });
        routes.Ignore("{*allhtml}", new { allhtml = @".*\.pdf(/.*)?" });
        routes.Ignore("{*allhtml}", new { allhtml = @".*\.aspx(/.*)?" });

        routes.MapPageRoute(
           "Home",               // Route name
           "",  // Route URL
           "~/Default.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "About",               // Route name
           "About",  // Route URL
           "~/About.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Caps",               // Route name
           "Caps",  // Route URL
           "~/Caps.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "CapsEpisode",               // Route name
           "Caps/{Episode}/{*StartNum}",  // Route URL
           "~/Caps.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Guides",               // Route name
           "Guides",  // Route URL
           "~/Guide.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "GuidesEpisode",               // Route name
           "Guides/{Episode}",  // Route URL
           "~/Guide.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Transcript",               // Route name
           "Guides/{EpisodeTranscript}/Transcript",  // Route URL
           "~/Guide.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Contact",               // Route name
           "Contact",  // Route URL
           "~/Contact.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Chat",               // Route name
           "Chat",  // Route URL
           "~/Chat.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Donate",               // Route name
           "Donate",  // Route URL
           "~/Donate.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Confirm",               // Route name
           "Confirm/{EmailConfirmation}",  // Route URL
           "~/Confirm.aspx"      // Web page to handle route
        );

        routes.MapPageRoute(
           "Reset",               // Route name
           "Reset/{EmailConfirmation}",  // Route URL
           "~/Reset.aspx"      // Web page to handle route
        );

        //routes.MapPageRoute(
        //   "News",               // Route name
        //   "News/",  // Route URL
        //   "~/Article.aspx"      // Web page to handle route
        //);

        //routes.MapPageRoute(
        //   "Article",               // Route name
        //   "News/{Year}/{Month}/{UrlLabel}",  // Route URL
        //   "~/Article.aspx"      // Web page to handle route
        //);

        routes.MapPageRoute(
           "Pages",               // Route name
           "{*PageUrl}",  // Route URL
           "~/Page.aspx"      // Web page to handle route
        );
    }

    private string StripKpfw(string val)
    {
        val = val.Replace(HttpUtility.UrlDecode("%E2%80%8B"), "");
        if (val.IndexOf("?kpfw") == -1)
            return val;
        return val.Substring(0, val.IndexOf("?kpfw")).Replace("?kpfw", "");
    }

</script>
