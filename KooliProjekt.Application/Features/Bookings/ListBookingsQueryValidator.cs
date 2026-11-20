using FluentValidation;

namespace KooliProjekt.Application.Features.Bookings
{
    public class ListBookingsQueryValidator
        : AbstractValidator<ListBookingsQuery>
    {
        public ListBookingsQueryValidator()
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