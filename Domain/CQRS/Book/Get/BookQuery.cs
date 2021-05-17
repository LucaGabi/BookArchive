using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive.Application.CQRS
{
    public class BookQuery : IRequest<CQRSResult<BookGetDTO>>
    {
        public int Id { get; set; }

        public class BookQueryHandler : IRequestHandler<BookQuery, CQRSResult<BookGetDTO>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IMapper mapper;

            public BookQueryHandler(IBookArchiveUOW uow, IMapper mapper)
            {
                this.uow = uow;
                this.mapper = mapper;
            }
            public async Task<CQRSResult<BookGetDTO>> Handle(BookQuery request, CancellationToken cancellationToken)
            {
                var book = uow.BooksRepository.GetById(request.Id);
                if (book != null) return mapper.Map<BookGetDTO>(book);
                else return mapper.Map<BookGetDTO>(book).AsResult(code: 404);
            }

        }
    }
}
