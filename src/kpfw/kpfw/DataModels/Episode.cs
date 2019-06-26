using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.DataModels
{
    [Table("Episode")]
    public class Episode
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required, MaxLength(150)]
        public string Title { get; set; }
        [Required, MaxLength(150)]
        public string UrlLabel { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
        public DateTime AirDate { get; set; }
        [MaxLength(15)]
        public string ProductionNumber { get; set; }
        [MaxLength(100)]
        public string Studio { get; set; }
        [MaxLength(300)]
        public string Writer { get; set; }
        [MaxLength(100)]
        public string Director { get; set; }
        [MaxLength(100)]
        public string Producer { get; set; }
        [MaxLength(200)]
        public string ExecutiveProducer { get; set; }
        [MaxLength(300)]
        public string Stars { get; set; }
        [MaxLength(300)]
        public string GuestStars { get; set; }
        [Column(TypeName = "varchar(MAX)"), MaxLength]
        public string Recap { get; set; }
        [Column(TypeName = "varchar(MAX)"), MaxLength]
        public string Transcript { get; set; }
    }
}
