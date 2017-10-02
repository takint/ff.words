namespace ff.words.application.ViewModels
{
    using ff.words.application.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class EntryViewModel : BaseViewModel
    {
        public string EntryTitle { get; set; }

        public string EntryContent { get; set; }

        public override void ValidateAndThrow()
        {
            throw new NotImplementedException();
        }
    }
}