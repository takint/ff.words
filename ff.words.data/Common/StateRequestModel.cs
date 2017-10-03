namespace ff.words.data.Common
{
    using System.Collections.Generic;

    public class StateRequestModel
    {
        public StateRequestModel()
        {
            Skip = 0;
            Take = 20;
            ShowDeleted = false;
            ListMode = null;
        }

        public FilterRequest Filter { get; set; }

        public IList<SortRequest> Sort { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public bool? ShowDeleted { get; set; }

        public int? ListMode { get; set; }
    }
}