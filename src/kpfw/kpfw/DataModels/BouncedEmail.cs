using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.DataModels
{
    [Table("BouncedEmail")]
    public class BouncedEmail
    {
        [Key,Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        public DateTime BounceDate { get; set; } = DateTime.UtcNow;
    }

    [Table("ComplainedEmail")]
    public class ComplainedEmail
    {
        [Key, Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        public DateTime ComplaintDate { get; set; } = DateTime.UtcNow;

    }
}
