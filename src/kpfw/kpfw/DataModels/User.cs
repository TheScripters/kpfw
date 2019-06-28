using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.DataModels
{
    [Table("User")]
    public class User
    {
        [Key, Required]
        public int Id { get; set; }
        [Required, MaxLength(40)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string UserEmail { get; set; }
        [Required, MaxLength(250)]
        public string UserPassword { get; set; }
        public DateTime JoinDate { get; set; }
        [Required]
        public string TimeZone { get; set; }
        public bool ShowEmail { get; set; }
        public string DisplayName { get; set; }
        public string IPAddress { get; set; }
        public bool IsActive { get; set; }
        public Guid? EmailConfirmation { get; set; }
        public string TwoFactor { get; set; }
    }
}
