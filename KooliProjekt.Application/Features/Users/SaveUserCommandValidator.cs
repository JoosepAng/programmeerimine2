using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUserCommandValidator
        : AbstractValidator<SaveUserCommand>
    {
        public SaveUserCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(command => command.Id)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.UserName)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(async (command, userName, cancellationToken) =>
                !await dbContext.Users.AnyAsync(
                    user => user.user_name == userName
                    && user.user_id != command.Id,
                    cancellationToken))
                .WithMessage("Selline kasutajanimi on juba kasutusel.");

            RuleFor(command => command.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(command => command.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(command => command.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(254);

            RuleFor(command => command.PasswordHash)
                .NotEmpty();

            RuleFor(command => command.RegisteredAt)
                .GreaterThan(System.DateTime.MinValue);

            RuleFor(command => command.Role)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}