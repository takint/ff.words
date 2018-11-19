namespace ff.words.data.Models
{
    using ff.words.data.Common;
    using System.ComponentModel.DataAnnotations;

    public class PageSettingModel : BaseEntity
    {
        [MaxLength(512)]
        public string SettingKey { get; set; }

        [MaxLength(512)]
        public string SettingName { get; set; }

        public string SettingValue { get; set; }
    }
}
