using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.DataModels
{
    [Table("Goof")]
    public class Goof
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Episode))]
        public int EpisodeId { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string GoofText { get; set; }

        [Required, ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public virtual Episode Episode { get; set; }
        public virtual User User { get; set; }
    }
}
