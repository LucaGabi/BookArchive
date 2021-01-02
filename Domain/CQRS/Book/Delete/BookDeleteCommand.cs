using BookArchive.DAL;
using BookArchive.DAL.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace BookArchive.Application.CQRS
{
    public class BookDeleteCommand: IRequest<CQRSResult<BookGetDTO>>
    {
        public int Id { get; set; }

        public class BookDeleteCommandHandler : IRequestHandler<BookDeleteCommand, CQRSResult<BookGetDTO>>
        {
            private readonly IBookArchiveUOW uow;

            public BookDeleteCommandHandler(IBookArchiveUOW uow)
            {
                this.uow = uow;
            }

            public async Task<CQRSResult<BookGetDTO>> Handle(BookDeleteCommand request, CancellationToken cancellationToken)
            {
                var book = uow.BooksRepository.GetById(request.Id);
                if (book != null) uow.BooksRepository.Delete(book,null);
                else return BookGetMap.ToDTO(book).AsCQRSResult(code: 404);
                await uow.Save(cancellationToken);
                return BookGetMap.ToDTO(book);
            }

        }
    }
}
