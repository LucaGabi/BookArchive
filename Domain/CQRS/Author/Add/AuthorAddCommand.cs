using BookArchive.DAL;
using BookArchive.DAL.Models;
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

            public AuthorAddCommandHandler(IBookArchiveUOW uow)
            {
                this.uow = uow;
            }

            public async Task<CQRSResult<AuthorGetDTO>> Handle(AuthorAddCommand request, CancellationToken cancellationToken)
            {
                var entity = request.ToModel();
                uow.AuthorsRepository.Add(entity,null);
                await uow.Save(cancellationToken);

                return AuthorGetMap.ToDTO(entity);
            }

        }
    }
}
