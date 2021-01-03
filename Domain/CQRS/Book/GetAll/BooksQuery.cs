using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.Application.CQRS
{
    public class BooksQuery : IRequest<CQRSResult<List<BookGetDTO>>>
    {
        public class BookQueryHandler : IRequestHandler<BooksQuery, CQRSResult<List<BookGetDTO>>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IMapper mapper;

            public BookQueryHandler(IBookArchiveUOW uow, IMapper mapper)
            {
                this.uow = uow;
                this.mapper = mapper;
            }
            public async Task<CQRSResult<List<BookGetDTO>>> Handle(BooksQuery request, CancellationToken cancellationToken)
            {
                var books = uow.BooksRepository.Get();
                return mapper.Map<List<BookGetDTO>>(books);
                //return books.Select(x=> BookGetMap.ToDTO(x)).ToList();
            }

        }
    }
}
