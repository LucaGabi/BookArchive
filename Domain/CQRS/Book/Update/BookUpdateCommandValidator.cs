
using FluentValidation;

namespace BookArchive.Application.CQRS
{
    public class BookUpdateCommandValidator : AbstractValidator<BookAddCommand>
    {
       
        public BookUpdateCommandValidator()
        {

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage($"Titlul este camp necesar.");

            RuleFor(v => v.Title)
                .Matches("[A-Za-z]+").WithMessage($"Titlul poate sa contina doar caractere.");

            RuleFor(v => v.Description)
               .NotEmpty().WithMessage($"Descrierea este camp necesar.");

            RuleFor(v => v.Description)
                .Matches("[A-Za-z]+").WithMessage($"Descrierea poate sa contina doar caractere.");
        }

    }
}
