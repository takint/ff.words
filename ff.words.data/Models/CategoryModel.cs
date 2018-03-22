namespace ff.words.data.Models
{
    using ff.words.data.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CategoryModel : BaseEntity
    {
        [MaxLength(512)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
