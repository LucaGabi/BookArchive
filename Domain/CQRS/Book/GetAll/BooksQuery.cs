using BookArchive.DAL;
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

            public BookQueryHandler(IBookArchiveUOW uow)
            {
                this.uow = uow;
            }
            public async Task<CQRSResult<List<BookGetDTO>>> Handle(BooksQuery request, CancellationToken cancellationToken)
            {
                var books = uow.BooksRepository.Get();
                return books.Select(x=> BookGetMap.ToDTO(x)).ToList();
            }

        }
    }
}
