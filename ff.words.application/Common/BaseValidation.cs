namespace ff.words.application.Common
{
    using FluentValidation;

    public abstract class BaseValidation<T> : AbstractValidator<T> where T : class
    {
    }
}