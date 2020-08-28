using Markdig;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpfw.Services
{
    [DebuggerDisplay("{" + nameof(CapEpisode) + "," + nameof(CapNumber) + "}")]
    public class Caps : LeafInline
    {
        public StringSlice CapNumber { get; set; }
        public StringSlice CapEpisode { get; set; }
    }
    public class CapsOptions
    {
        public CapsOptions()
        {

        }
    }
    public class CapsRenderer : HtmlObjectRenderer<Caps>
    {
        private CapsOptions _options;

        public CapsRenderer(CapsOptions options)
        {
            _options = options;
        }

        public CapsRenderer()
        {
        }

        protected override void Write(HtmlRenderer renderer, Caps obj)
        {
            StringSlice CapNumber;
            StringSlice CapEpisode;

            CapNumber = obj.CapNumber;
            CapEpisode = obj.CapEpisode;
            var ep = EpCapsList.S1List.Where(e => e.UrlLabel == CapEpisode.Text);
            if (ep.Count() == 0)
                ep = EpCapsList.S2List.Where(e => e.UrlLabel == CapEpisode.Text);
            if (ep.Count() == 0)
                ep = EpCapsList.S3List.Where(e => e.UrlLabel == CapEpisode.Text);
            if (ep.Count() == 0)
                ep = EpCapsList.S4List.Where(e => e.UrlLabel == CapEpisode.Text);
            if (ep.Count() == 0)
                ep = EpCapsList.MovieList.Where(e => e.UrlLabel == CapEpisode.Text);
            if (ep.Count() == 0)
                ep = EpCapsList.MiscList.Where(e => e.UrlLabel == CapEpisode.Text);

            if (ep.Count() == 0)
                return; // The user supplied an invalid argument

            var epUrl = ep.First().CDNPath;

            if (renderer.EnableHtmlForInline)
            {
                renderer.Write("<a href=\"https://cdn.kpfanworld.com/caps/").Write(epUrl).Write("/Image").Write(CapNumber).Write(".jpg\">");
                renderer.Write("<img src=\"https://cdn.kpfanworld.com/caps/").Write(epUrl).Write("/thumbs/Image").Write(CapNumber).Write("_thumb.jpg\" />");
                renderer.Write("</a>");
            }
            else
            {
                renderer.Write("{").Write(CapEpisode).Write(", ").Write(CapNumber).Write("}");
            }

            //if (renderer.EnableHtmlForInline)
            //{
            //    renderer.Write("<a href=\"").Write(_options.Url).Write("view.php?id=").Write(issueNumber).Write('"');

            //    if (_options.OpenInNewWindow)
            //    {
            //        renderer.Write(" target=\"blank\" rel=\"noopener noreferrer\"");
            //    }

            //    renderer.Write('>').Write('#').Write(issueNumber).Write("</a>");
            //}
            //else
            //{
            //    renderer.Write('#').Write(obj.IssueNumber);
            //}
        }
    }
    public class CapsInlineParser : InlineParser
    {
        private static readonly char[] _openingCharacters = "{".ToCharArray();
        public CapsInlineParser()
        {
            this.OpeningCharacters = _openingCharacters;
        }
        public override bool Match(InlineProcessor processor, ref StringSlice slice)
        {
            bool matchFound;
            char previous;

            matchFound = false;
            previous = slice.PeekCharExtra(-1);

            if (!previous.IsWhiteSpaceOrZero())
            {
                return false;
            }

            char current;
            int start;
            int end;

            slice.NextChar();

            current = slice.CurrentChar;
            start = slice.Start;
            end = start;

            StringBuilder ep = new StringBuilder();
            StringBuilder num = new StringBuilder();

            while (current != ',')
            {
                end = slice.Start;
                ep.Append(current);
                current = slice.NextChar();
            }

            current = slice.NextChar();

            while (current != '}')
            {
                end = slice.Start;
                num.Append(current);
                current = slice.NextChar();
            }

            current = slice.NextChar();

            if (current.IsWhiteSpaceOrZero())
            {
                int inlineStart;
                inlineStart = processor.GetSourcePosition(slice.Start, out int line, out int column);

                processor.Inline = new Caps() { Span = { Start = inlineStart, End = inlineStart + (end - start) + 1 }, Line = line, Column = column, CapEpisode = new StringSlice(ep.ToString()), CapNumber = new StringSlice(num.ToString()) };

                matchFound = true;
            }

            return matchFound;
        }
    }
    public class CapsExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            OrderedList<InlineParser> parsers;

            parsers = pipeline.InlineParsers;

            if (!parsers.Contains<CapsInlineParser>())
            {
                parsers.Add(new CapsInlineParser());
            }
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            HtmlRenderer htmlRenderer;
            ObjectRendererCollection renderers;

            htmlRenderer = renderer as HtmlRenderer;
            renderers = htmlRenderer?.ObjectRenderers;

            if (renderers != null && !renderers.Contains<CapsRenderer>())
            {
                renderers.Add(new CapsRenderer());
            }
        }
    }
}
