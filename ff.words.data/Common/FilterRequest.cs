namespace ff.words.data.Common
{
    using System.Collections.Generic;

    public class FilterRequest
    {
        public FilterRequest()
        {
            Filters = new List<FilterDetail>();
        }

        public string Logic { get; set; }

        public IList<FilterDetail> Filters { get; set; }
    }
}
