using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace BookArchive.Application.CQRS
{
    public class AuthorUpdateCommand : AuthorUpdateDTO, IRequest<CQRSResult<AuthorGetDTO>>
    {

        public class AuthorUpdateCommandHandler : IRequestHandler<AuthorUpdateCommand, CQRSResult<AuthorGetDTO>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IMapper mapper;

            public AuthorUpdateCommandHandler(IBookArchiveUOW uow, IMapper mapper)
            {
                this.uow = uow;
                this.mapper = mapper;
            }

            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorUpdateCommand request, CancellationToken cancellationToken)
            {
                var exists = uow.AuthorsRepository.Contains(x => x.Id == request.Id);
                if (exists)
                {
                    var entity = mapper.Map<Author>(request);
                    uow.AuthorsRepository.Update(entity);
                    await uow.Save();
                    return mapper.Map<AuthorGetDTO>(entity);
                }
                else
                    return mapper.Map<AuthorGetDTO>(request).AsResult(code: 404);
            }


        }
    }
}
