using AutoMapper;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace BookArchive.Application.CQRS
{
    public class BookAddCommand : BookAddDTO, IRequest<CQRSResult<BookGetDTO>>
    {
        private Stream coverImage;
        public void SetCoverImage(Stream value)
        {
            coverImage = value;
        }

        public class BookAddCommandHandler : IRequestHandler<BookAddCommand, CQRSResult<BookGetDTO>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IFileStorageService fileStorageService;
            private readonly IMapper mapper;

            public BookAddCommandHandler(IBookArchiveUOW uow, IFileStorageService fileStorageService, IMapper mapper)
            {
                this.uow = uow;
                this.fileStorageService = fileStorageService;
                this.mapper = mapper;
            }

            public async Task<CQRSResult<BookGetDTO>> Handle(BookAddCommand request, CancellationToken cancellationToken)
            {
                if (request.coverImage.Length > 0)
                {
                    var fExt = Path.GetExtension(request.CoverImagePath);
                    var fNewName = Guid.NewGuid().ToString() + fExt;
                    request.CoverImagePath = await fileStorageService.Save(request.coverImage, fNewName);
                }

                var entity = mapper.Map<Book>(request);
                uow.BooksRepository.Add(entity);
                await uow.Save(cancellationToken);
                return mapper.Map<BookGetDTO>(entity);
            }


        }
    }
}
