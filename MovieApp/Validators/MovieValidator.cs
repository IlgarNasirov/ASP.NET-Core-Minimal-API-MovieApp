using FluentValidation;
using MovieApp.DTOs;

namespace MovieApp.Validators
{
    public class MovieValidator:AbstractValidator<MovieDTO>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(30);
            RuleFor(x => x.Budget).NotEmpty();
            RuleFor(x => x.Budget).InclusiveBetween(1, double.MaxValue);
            RuleFor(x => x.Genre).NotEmpty();
            RuleFor(x => x.Genre).MaximumLength(30);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}