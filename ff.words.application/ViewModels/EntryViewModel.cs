﻿namespace ff.words.application.ViewModels
{
    using ff.words.application.AutoMapper;
    using ff.words.application.Common;
    using ff.words.application.Validations;
    using ff.words.data.Common;
    using ff.words.data.Models;
    using FluentValidation;
    using global::AutoMapper;

    public class EntryViewModel : BaseViewModel, ICreateMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Excerpt { get; set; }

        public string FeaturedImage { get; set; }

        public string ThumbnailImage { get; set; }

        public string AuthorName { get; set; }

        public EntryStatus CurrentStatus { get; set; }

        public override void ValidateAndThrow()
        {
            new EntryValidation().ValidateAndThrow(this);
        }

        public void CreateMapping(Profile profile)
        {
            profile.CreateMap<EntryModel, EntryViewModel>()
                .IncludeBase<BaseEntity, BaseViewModel>();

            profile.CreateMap<EntryViewModel, EntryModel>()
                .IncludeBase<BaseViewModel, BaseEntity>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}