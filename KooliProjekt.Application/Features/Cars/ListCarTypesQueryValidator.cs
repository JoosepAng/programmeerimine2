using FluentValidation;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class ListCarTypesQueryValidator
        : AbstractValidator<ListCarTypesQuery>
    {
        public ListCarTypesQueryValidator()
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