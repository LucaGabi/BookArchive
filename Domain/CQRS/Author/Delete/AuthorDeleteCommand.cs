using AutoMapper;
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
            private readonly IMapper mapper;

            public AuthorDeleteCommandHandler(IBookArchiveUOW uow, IMapper mapper)
            {
                this.uow = uow;
                this.mapper = mapper;
            }

            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorDeleteCommand request, CancellationToken cancellationToken)
            {
                var author = uow.AuthorsRepository.GetById(request.Id);
                if (author != null) uow.AuthorsRepository.Delete(author);
                else return new AuthorGetDTO { Id = request.Id }.AsResult(code: 404);
                await uow.Save(cancellationToken);
                return new AuthorGetDTO { Id = request.Id };
            }


        }
    }
}
