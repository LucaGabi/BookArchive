using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace BookArchive.Application.CQRS
{
    public class BookDeleteCommand : IRequest<CQRSResult<BookGetDTO>>
    {
        public int Id { get; set; }

        public class BookDeleteCommandHandler : IRequestHandler<BookDeleteCommand, CQRSResult<BookGetDTO>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IMapper mapper;

            public BookDeleteCommandHandler(IBookArchiveUOW uow, IMapper mapper)
            {
                this.uow = uow;
                this.mapper = mapper;
            }

            public async Task<CQRSResult<BookGetDTO>> Handle(BookDeleteCommand request, CancellationToken cancellationToken)
            {
                var book = uow.BooksRepository.GetById(request.Id);
                if (book != null) uow.BooksRepository.Delete(book);
                else return mapper.Map<BookGetDTO>(book).AsResult(code: 404);
                await uow.Save(cancellationToken);
                return mapper.Map<BookGetDTO>(book);
            }

        }
    }
}
