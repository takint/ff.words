namespace ff.words.data.Models
{
    using ff.words.data.Common;
    using System.ComponentModel.DataAnnotations;

    public class EntryModel : BaseEntity
    {
        [MaxLength(512)]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Excerpt { get; set; }

        public string FeaturedImage { get; set; }

        [MaxLength(100)]
        public string AuthorName { get; set; }

        public EntryStatus CurrentStatus { get; set; }
    }
}
