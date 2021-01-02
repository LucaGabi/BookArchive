using AutoMapper;
using BookArchive.DAL;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.Application.CQRS
{
    public class AuthorsQuery : IRequest<CQRSResult<List<AuthorGetDTO>>>
    {
        public class AuthorQueryHandler : IRequestHandler<AuthorsQuery, CQRSResult<List<AuthorGetDTO>>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IMapper mapper;

            public AuthorQueryHandler(IBookArchiveUOW uow, IMapper mapper)
            {
                this.uow = uow;
                this.mapper = mapper;
            }
            public async Task<CQRSResult<List<AuthorGetDTO>>> Handle(AuthorsQuery request, CancellationToken cancellationToken)
            {
                var authors = uow.AuthorsRepository.Get();
                return mapper.Map<List<AuthorGetDTO>>(authors);
            }

        }
    }
}
