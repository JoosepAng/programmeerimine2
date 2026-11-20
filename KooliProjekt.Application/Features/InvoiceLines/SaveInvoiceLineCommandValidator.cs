using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class SaveInvoiceLineCommandValidator
        : AbstractValidator<SaveInvoiceLineCommand>
    {
        public SaveInvoiceLineCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.InvoiceId)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Invoices.AnyAsync(
                        invoice => invoice.invoice_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga arvet ei leitud.");

            RuleFor(command => command.Description)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(command => command.Quantity)
                .GreaterThan(0);

            RuleFor(command => command.UnitPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Amount)
                .GreaterThanOrEqualTo(0);
        }
    }
}