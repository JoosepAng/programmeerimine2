using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class DeleteInvoiceLineCommandValidator
        : AbstractValidator<DeleteInvoiceLineCommand>
    {
        public DeleteInvoiceLineCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .MustAsync((id, cancellationToken) =>
                    dbContext.InvoiceLines.AnyAsync(
                        line => line.invoice_line_id == id,
                        cancellationToken))
                .WithMessage("Sellise ID-ga arverida ei leitud.");
        }
    }
}