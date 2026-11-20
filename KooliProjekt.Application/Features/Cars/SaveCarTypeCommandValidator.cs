using FluentValidation;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class SaveCarTypeCommandValidator
        : AbstractValidator<SaveCarTypeCommand>
    {
        public SaveCarTypeCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(command => command.Description)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}