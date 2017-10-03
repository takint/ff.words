namespace ff.words.data.Common
{
    public struct SortRequest
    {
        public SortRequest(string field, string dir)
        {
            Field = field;
            Dir = dir;
        }

        public string Field { get; set; }

        public string Dir { get; set; }
    }
}