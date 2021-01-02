using BookArchive.DAL;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.Application.CQRS
{
    public class AuthorQuery: IRequest<CQRSResult<AuthorGetDTO>>
    {
        public int Id { get; set; }

        public class AuthorQueryHandler : IRequestHandler<AuthorQuery, CQRSResult<AuthorGetDTO>>
        {
            private readonly IBookArchiveUOW uow;

            public AuthorQueryHandler(IBookArchiveUOW uow)
            {
                this.uow = uow;
            }
            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorQuery request, CancellationToken cancellationToken)
            {
                var author = uow.AuthorsRepository.GetById(request.Id);
                if (author != null) return AuthorGetMap.ToDTO(author);
                else return (null as AuthorGetDTO).AsCQRSResult(code: 404); ;
            }

        }
    }
}
