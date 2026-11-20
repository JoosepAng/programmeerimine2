using FluentValidation;

namespace KooliProjekt.Application.Features.Cars
{
    public class ListCarsQueryValidator : AbstractValidator<ListCarsQuery>
    {
        public ListCarsQueryValidator()
        {
            RuleFor(query => query.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Lehekülje number peab olema vähemalt 1.");

            RuleFor(query => query.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Lehekülje suurus peab olema vähemalt 1.");
        }
    }
}