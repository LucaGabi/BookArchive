using BookArchive.DAL;
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

            public AuthorUpdateCommandHandler(IBookArchiveUOW uow)
            {
                this.uow = uow;
            }

            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorUpdateCommand request, CancellationToken cancellationToken)
            {
                var exists = uow.AuthorsRepository.Contains(x => x.Id == request.Id);
                if (exists)
                {
                    var entity = request.ToModel();
                    uow.AuthorsRepository.Update(entity);
                    await uow.Save();
                    return AuthorGetMap.ToDTO(entity);
                }
                else 
                    return (null as AuthorGetDTO).AsCQRSResult(code: 404);
            }


        }
    }
}
