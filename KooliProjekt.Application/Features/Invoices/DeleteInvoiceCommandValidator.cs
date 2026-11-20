using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class DeleteInvoiceCommandValidator
        : AbstractValidator<DeleteInvoiceCommand>
    {
        public DeleteInvoiceCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.Invoices.AnyAsync(
                        invoice => invoice.invoice_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga arvet ei leitud.");
        }
    }
}