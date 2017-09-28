namespace ff.words.application.Common
{
    using System.Collections.Generic;

    public class ListResponseModel<T> where T : BaseViewModel
    {
        public int RecordCount { get; set; }
        public IEnumerable<T> ListResult { get; set; }
    }
}