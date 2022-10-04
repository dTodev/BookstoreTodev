using Bookstore.Models.Requests;
using FluentValidation;

namespace Bookstore.Validators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Age).NotNull().GreaterThan(0).LessThanOrEqualTo(120);
            When(x => !string.IsNullOrEmpty(x.NickName), () =>
            {
                RuleFor(x => x.NickName).MinimumLength(2).MaximumLength(10);
            });
            RuleFor(x=> x.DateOfBirth).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
        }
    }
}
