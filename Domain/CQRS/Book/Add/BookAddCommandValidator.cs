
using FluentValidation;

namespace BookArchive.Application.CQRS
{
    public class BookAddCommandValidator : AbstractValidator<BookAddCommand>
    {
       
        public BookAddCommandValidator()
        {

        }

    }
}
