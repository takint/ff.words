namespace ff.words.application.Common
{
    public class ListRequestModel
    {
        private string _searchCriteria;

        private string _sortField;

        public ListRequestModel()
        {
            SkipRecords = 0;
            TakeCount = 10;
        }

        public virtual string SearchCriteria
        {
            get => _searchCriteria;

            set => _searchCriteria = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        public virtual string SortField
        {
            get => _sortField;
            set => _sortField = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        public int? SkipRecords { get; set; }

        public int? TakeCount { get; set; }

        public string State { get; set; }
    }
}