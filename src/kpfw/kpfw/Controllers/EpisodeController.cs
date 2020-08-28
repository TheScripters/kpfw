using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using kpfw.DataModels;
using kpfw.Models;
using kpfw.Services;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace kpfw.Controllers
{
    public class EpisodeController : Controller
    {
        protected MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseCaps().Build();
        private readonly DataContext _context;
        public EpisodeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (RouteData.Values["Episode"] == null)
            {
                var eps = _context.Episodes.ToList();
                List<EpisodeViewModel> model = new List<EpisodeViewModel>();
                foreach (var ep in eps)
                    model.Add(new EpisodeViewModel(ep));

                return View("EpisodeIndex", model);
            }
            else
            {
                var ep = _context.Episodes.Where(x => x.UrlLabel == RouteData.Values["Episode"].ToString()).FirstOrDefault();
                if (ep == null)
                    return Redirect("/Guides");

                var model = new EpisodeViewModel(ep);
                switch (model.Season)
                {
                    case 1:
                        model.CapsUrl = EpCapsList.S1List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                        break;
                    case 2:
                        model.CapsUrl = EpCapsList.S2List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                        break;
                    case 3:
                        model.CapsUrl = EpCapsList.S3List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                        break;
                    case 4:
                        model.CapsUrl = EpCapsList.S4List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                        break;
                }
                for (var s = 0; s < model.Director.Length; s++)
                {
                    string v = model.Director[s];
                    string id = _context.CrewLinks.Where(c => c.CrewName == v).SingleOrDefault()?.ImdbNameID;
                    if (!String.IsNullOrWhiteSpace(id))
                        model.Director[s] = $@"<a href=""//www.imdb.com/name/nm{id}/"" target=""_blank"" rel=""nofollow"">{v}</a>";
                }
                for (var s = 0; s < model.Writer.Length; s++)
                {
                    string v = model.Writer[s];
                    string id = _context.CrewLinks.Where(c => c.CrewName == v).SingleOrDefault()?.ImdbNameID;
                    if (!String.IsNullOrWhiteSpace(id))
                        model.Writer[s] = $@"<a href=""//www.imdb.com/name/nm{id}/"" target=""_blank"" rel=""nofollow"">{v}</a>";
                }
                for (var s = 0; s < model.Stars.Length; s++)
                {
                    string v = model.Stars[s];
                    string id = _context.CrewLinks.Where(c => c.CrewName == v).SingleOrDefault()?.ImdbNameID;
                    if (!String.IsNullOrWhiteSpace(id))
                        model.Stars[s] = $@"<a href=""//www.imdb.com/name/nm{id}/"" target=""_blank"" rel=""nofollow"">{v}</a>";
                }
                for (var s = 0; s < model.GuestStars.Length; s++)
                {
                    string v = model.GuestStars[s];
                    string id = _context.CrewLinks.Where(c => c.CrewName == v).SingleOrDefault()?.ImdbNameID;
                    if (!String.IsNullOrWhiteSpace(id))
                        model.GuestStars[s] = $@"<a href=""//www.imdb.com/name/nm{id}/"" target=""_blank"" rel=""nofollow"">{v}</a>";
                }
                for (var s = 0; s < model.ExecutiveProducer.Length; s++)
                {
                    string v = model.ExecutiveProducer[s];
                    string id = _context.CrewLinks.Where(c => c.CrewName == v).SingleOrDefault()?.ImdbNameID;
                    if (!String.IsNullOrWhiteSpace(id))
                        model.ExecutiveProducer[s] = $@"<a href=""//www.imdb.com/name/nm{id}/"" target=""_blank"" rel=""nofollow"">{v}</a>";
                }
                for (var s = 0; s < model.Producer.Length; s++)
                {
                    string v = model.Producer[s];
                    string id = _context.CrewLinks.Where(c => c.CrewName == v).SingleOrDefault()?.ImdbNameID;
                    if (!String.IsNullOrWhiteSpace(id))
                        model.Producer[s] = $@"<a href=""//www.imdb.com/name/nm{id}/"" target=""_blank"" rel=""nofollow"">{v}</a>";
                }
                foreach (var n in _context.Notes.Where(n => n.EpisodeId == model.Id))
                {
                    n.NoteText = Markdown.ToHtml(n.NoteText.Nl2Br(), pipeline);
                    model.Notes.Add(n);
                }
                foreach (var n in _context.Quotes.Where(n => n.EpisodeId == model.Id))
                {
                    n.QuoteText = Markdown.ToHtml(n.QuoteText.Nl2Br(), pipeline);
                    model.Quotes.Add(n);
                }
                foreach (var n in _context.Goofs.Where(n => n.EpisodeId == model.Id))
                {
                    n.GoofText = Markdown.ToHtml(n.GoofText.Nl2Br(), pipeline);
                    model.Goofs.Add(n);
                }
                foreach (var n in _context.Culturals.Where(n => n.EpisodeId == model.Id))
                {
                    n.CulturalText = Markdown.ToHtml(n.CulturalText.Nl2Br(), pipeline);
                    model.Culturals.Add(n);
                }

                return View("Episode", model);
            }
        }

        public IActionResult Transcript()
        {
            var ep = _context.Episodes.Where(x => x.UrlLabel == RouteData.Values["Episode"].ToString()).FirstOrDefault();
            if (ep == null)
                return Redirect("/Guides");

            var model = new EpisodeViewModel(ep);
            if (!model.HasTranscript)
                return RedirectToAction("Index", "Episode", new { Episode = ep.UrlLabel });

            switch (model.Season)
            {
                case 1:
                    model.CapsUrl = EpCapsList.S1List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                    break;
                case 2:
                    model.CapsUrl = EpCapsList.S2List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                    break;
                case 3:
                    model.CapsUrl = EpCapsList.S3List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                    break;
                case 4:
                    model.CapsUrl = EpCapsList.S4List.Where(p => p.UrlLabel == model.UrlLabel).SingleOrDefault()?.UrlLabel;
                    break;
            }

            model.Transcript = Markdown.ToHtml(model.Transcript.Nl2Br(), pipeline);

            return View("EpisodeTranscript", model);
        }
    }
}