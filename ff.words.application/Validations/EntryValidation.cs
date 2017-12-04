namespace ff.words.application.Validations
{
    using ff.words.application.Common;
    using ff.words.application.ViewModels;
    using FluentValidation;

    public class EntryValidation : BaseValidation<EntryViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryValidation"/> class.
        /// </summary>
        public EntryValidation()
        {
            RuleFor(c => c.Title).NotEmpty();

            RuleFor(c => c.CreatedUser).NotEmpty();
            RuleFor(c => c.CreatedDate).NotEmpty();

            RuleFor(c => c.RowVersion).NotEmpty().When(c => c.Id > 0);
            RuleFor(c => c.UpdatedUser).NotEmpty().When(c => c.Id > 0);
            RuleFor(c => c.UpdatedDate).NotEmpty().When(c => c.Id > 0);
        }
    }
}