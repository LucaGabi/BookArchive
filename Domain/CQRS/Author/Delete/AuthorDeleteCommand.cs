using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.Application.CQRS
{
    public class AuthorDeleteCommand : IRequest<CQRSResult<AuthorGetDTO>>
    {
        public int Id { get; set; }

        public class AuthorDeleteCommandHandler : IRequestHandler<AuthorDeleteCommand, CQRSResult<AuthorGetDTO>>
        {
            private readonly IBookArchiveUOW uow;

            public AuthorDeleteCommandHandler(IBookArchiveUOW uow)
            {
                this.uow = uow;
            }

            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorDeleteCommand request, CancellationToken cancellationToken)
            {
                var author = uow.AuthorsRepository.GetById(request.Id);
                if (author != null) uow.AuthorsRepository.Delete(author);
                else return AuthorGetMap.ToDTO(null).AsCQRSResult(code: 404);
                await uow.Save(cancellationToken);
                return AuthorGetMap.ToDTO(new Author { Id = request.Id });
            }


        }
    }
}
