using ff.words.application.ViewModels;
using System.Collections.Generic;

namespace ff.words.pages.Models
{
    public class HomeModel
    {
        public IEnumerable<EntryViewModel> ListEntries { get; set; }
    }
}
