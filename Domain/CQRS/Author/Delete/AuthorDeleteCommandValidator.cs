
using FluentValidation;


namespace BookArchive.Application.CQRS
{
    public class AuthorDeleteCommandValidator : AbstractValidator<AuthorDeleteCommand>
    {
       
        public AuthorDeleteCommandValidator()
        {
            
        }

    }
}
