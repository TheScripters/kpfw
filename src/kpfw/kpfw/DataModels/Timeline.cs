using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.DataModels
{
    [Table("Timeline")]
    public class Timeline
    {
        [Key,Required]
        public int Id { get; set; }

        [Column(TypeName = "Date"), Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(300)]
        public string Message { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
