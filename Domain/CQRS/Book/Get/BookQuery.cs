using BookArchive.DAL;
using BookArchive.DAL.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.Application.CQRS
{
    public class BookQuery :  IRequest<CQRSResult<BookGetDTO>>
    {
        public int Id { get; set; }

        public class BookQueryHandler : IRequestHandler<BookQuery, CQRSResult<BookGetDTO>>
        {
            private readonly IBookArchiveUOW uow;

            public BookQueryHandler(IBookArchiveUOW uow)
            {
                this.uow = uow;
            }
            public async Task<CQRSResult<BookGetDTO>> Handle(BookQuery request, CancellationToken cancellationToken)
            {
                var book = uow.BooksRepository.GetById(request.Id);
                if (book != null) return BookGetMap.ToDTO(book);
                else return BookGetMap.ToDTO(book).AsCQRSResult(code: 404);
            }

        }
    }
}
