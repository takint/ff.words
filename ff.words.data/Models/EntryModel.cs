namespace ff.words.data.Models
{
    using ff.words.data.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EntryModel : BaseEntity
    {
        [MaxLength(512)]
        public string EntryTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string EntryContent { get; set; }
    }
}
