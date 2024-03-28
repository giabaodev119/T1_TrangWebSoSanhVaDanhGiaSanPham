using System.ComponentModel.DataAnnotations;

namespace DACS.Models.EF
{
    public class SystemSetting
    {
        [Key]
        [StringLength(50)]
        public string SettingKey { get; set; }
        [StringLength(4000)]
        public string SettingValue { get; set; }
        [StringLength(4000)]
        public string SettingDescripstion {  get; set; }
    }
}
