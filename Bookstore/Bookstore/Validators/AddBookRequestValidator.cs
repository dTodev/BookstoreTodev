using Bookstore.Models.Requests;
using FluentValidation;

namespace Bookstore.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x=>x.Title).NotEmpty().MinimumLength(2).MaximumLength(66);
            RuleFor(x => x.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}
