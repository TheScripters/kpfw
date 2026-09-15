using kpfw.DataModels;
using kpfw.Services;
using System;
using System.Collections.Generic;

namespace kpfw.Models
{
    public class EpisodeViewModel(Episode episode)
    {
        public int Id { get; set; } = episode.Id;
        public int Number { get; set; } = episode.Number;
        public string Title { get; set; } = episode.Title;
        public string UrlLabel { get; set; } = episode.UrlLabel;
        public string Description { get; set; } = episode.Description;
        public DateTime AirDate { get; set; } = episode.AirDate;
        public string ProductionNumber { get; set; } = episode.ProductionNumber;
        public string Studio { get; set; } = episode.Studio;
        public string[] Writer { get; set; } = [.. episode.Writer.Split([','], StringSplitOptions.RemoveEmptyEntries).Trim()];
        public string[] Director { get; set; } = [.. episode.Director.Split([','], StringSplitOptions.RemoveEmptyEntries).Trim()];
        public string[] Producer { get; set; } = [.. episode.Producer.Split([','], StringSplitOptions.RemoveEmptyEntries).Trim()];
        public string[] ExecutiveProducer { get; set; } = [.. episode.ExecutiveProducer.Split([','], StringSplitOptions.RemoveEmptyEntries).Trim()];
        public string[] Stars { get; set; } = [.. episode.Stars.Split([','], StringSplitOptions.RemoveEmptyEntries).Trim()];
        public string[] GuestStars { get; set; } = [.. episode.GuestStars.Split([','], StringSplitOptions.RemoveEmptyEntries).Trim()];
        public string Recap { get; set; } = episode.Recap;
        public string Transcript { get; set; } = episode.Transcript;
        public int Season { get { return Convert.ToInt32(ProductionNumber[0].ToString()); } }
        public bool HasTranscript { get; set; } = !String.IsNullOrWhiteSpace(episode.Transcript);
        public string CapsUrl { get; set; }
        public List<Note> Notes { get; set; } = [];
        public List<Quote> Quotes { get; set; } = [];
        public List<Goof> Goofs { get; set; } = [];
        public List<Cultural> Culturals { get; set; } = [];
    }
}
