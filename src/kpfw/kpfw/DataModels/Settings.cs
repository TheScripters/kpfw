using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kpfw.DataModels
{
    [Table("Settings")]
    public class Settings
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string SettingName { get; set; }

        [Required]
        public string SettingValue { get; set; }
    }
}
