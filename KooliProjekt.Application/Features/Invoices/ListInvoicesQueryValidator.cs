using FluentValidation;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoicesQueryValidator
        : AbstractValidator<ListInvoicesQuery>
    {
        public ListInvoicesQueryValidator()
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