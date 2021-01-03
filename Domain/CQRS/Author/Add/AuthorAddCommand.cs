using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.Application.CQRS
{
    public class AuthorAddCommand : AuthorAddDTO, IRequest<CQRSResult<AuthorGetDTO>>
    {
        public class AuthorAddCommandHandler : IRequestHandler<AuthorAddCommand, CQRSResult<AuthorGetDTO>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IMapper mapper;

            public AuthorAddCommandHandler(IBookArchiveUOW uow, IMapper mapper)
            {
                this.uow = uow;
                this.mapper = mapper;
            }

            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorAddCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Author>(request);
                uow.AuthorsRepository.Add(entity);
                await uow.Save(cancellationToken);

                return mapper.Map<AuthorGetDTO>(entity);
            }

        }
    }
}
