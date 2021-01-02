
using FluentValidation;


namespace BookArchive.Application.CQRS
{
    public class BooksDeleteCommandValidator : AbstractValidator<BookDeleteCommand>
    {
       
        public BooksDeleteCommandValidator()
        {
            
        }

    }
}
