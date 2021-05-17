
using FluentValidation;

namespace BookArchive.Application.CQRS
{
    public class AuthorAddCommandValidator : AbstractValidator<AuthorAddCommand>
    {
       
        public AuthorAddCommandValidator()
        {

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage($"Nume este camp necesar.");

            RuleFor(v => v.Name)
                .Matches("[A-Za-z]+").WithMessage($"Nume poate sa contina doar caractere.");

        }

    }
}
