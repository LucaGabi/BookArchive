
using FluentValidation;

namespace BookArchive.Application.CQRS
{
    public class AuthorUpdateCommandValidator : AbstractValidator<AuthorUpdateCommand>
    {
       
        public AuthorUpdateCommandValidator()
        {

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage($"Nume este camp necesar.");

            RuleFor(v => v.Name)
                .Matches("[A-Za-z]+").WithMessage($"Nume poate sa contina doar caractere.");

        }

    }
}
