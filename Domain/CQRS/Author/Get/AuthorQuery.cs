using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using BookArchive.DAL;

using MediatR;

namespace BookArchive.Application.CQRS {
    public class AuthorQuery : IRequest<CQRSResult<AuthorGetDTO>> {
        public int Id { get; set; }

        public class AuthorQueryHandler : IRequestHandler<AuthorQuery, CQRSResult<AuthorGetDTO>> {
            private readonly IBookArchiveUOW uow;
            private readonly IMapper mapper;

            public AuthorQueryHandler(IBookArchiveUOW uow, IMapper mapper) {
                this.uow = uow;
                this.mapper = mapper;
            }
            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorQuery request, CancellationToken cancellationToken) {
                var author = uow.AuthorsRepository.GetById(request.Id);
                if (author != null) return mapper.Map<AuthorGetDTO>(author);
                else return (new AuthorGetDTO { Id = request.Id })
                    .AsResult(code: 404);
            }

        }
    }
}