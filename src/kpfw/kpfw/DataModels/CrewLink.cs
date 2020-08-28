using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.DataModels
{
    [Table("CrewLink")]
    public class CrewLink
    {
        [Key, Required]
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string CrewName { get; set; }

        [Required,MaxLength(10)]
        public string ImdbNameID { get; set; }
    }
}
