using kpfw.DataModels;
using kpfw.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.Models
{
    public class EpisodeViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string UrlLabel { get; set; }
        public string Description { get; set; }
        public DateTime AirDate { get; set; }
        public string ProductionNumber { get; set; }
        public string Studio { get; set; }
        public string[] Writer { get; set; }
        public string[] Director { get; set; }
        public string[] Producer { get; set; }
        public string[] ExecutiveProducer { get; set; }
        public string[] Stars { get; set; }
        public string[] GuestStars { get; set; }
        public string Recap { get; set; }
        public string Transcript { get; set; }
        public int Season { get { return Convert.ToInt32(ProductionNumber[0].ToString()); } }
        public bool HasTranscript { get; set; }
        public string CapsUrl { get; set; }

        public EpisodeViewModel(Episode episode)
        {
            Id = episode.Id;
            Number = episode.Number;
            Title = episode.Title;
            AirDate = episode.AirDate;
            ProductionNumber = episode.ProductionNumber;
            Stars = episode.Stars.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray();
            GuestStars = episode.GuestStars.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray();
            Producer = episode.Producer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray();
            ExecutiveProducer = episode.ExecutiveProducer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray();
            Description = episode.Description;
            Writer = episode.Writer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray();
            Studio = episode.Studio;
            UrlLabel = episode.UrlLabel;
            Director = episode.Director.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray();
            Recap = episode.Recap;
            Transcript = episode.Transcript;
            HasTranscript = !String.IsNullOrWhiteSpace(episode.Transcript);
        }
    }
}
