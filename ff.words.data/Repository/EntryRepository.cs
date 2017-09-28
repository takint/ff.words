namespace ff.words.data.Repository
{
    using ff.words.data.Context;
    using ff.words.data.Interfaces;
    using ff.words.data.Models;

    public class EntryRepository : Repository<EntryModel>, IEntryRepository
    {
        public EntryRepository(FFWordsContext context)
            : base(context)
        {
        }
    }
}