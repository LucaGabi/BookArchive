using BookArchive.DAL;
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

            public BookAddCommandHandler(IBookArchiveUOW uow, IFileStorageService fileStorageService)
            {
                this.uow = uow;
                this.fileStorageService = fileStorageService;
            }

            public async Task<CQRSResult<BookGetDTO>> Handle(BookAddCommand request, CancellationToken cancellationToken)
            {
                if (request.coverImage.Length > 0)
                {
                    var fExt = Path.GetExtension(request.CoverImagePath);
                    var fNewName = Guid.NewGuid().ToString() + fExt;
                    request.CoverImagePath = await fileStorageService.Save(request.coverImage, fNewName);
                }
               
                var entity = BookAddMap.ToModel(request);
                uow.BooksRepository.Add(entity,null);
                await uow.Save(cancellationToken);
                return BookGetMap.ToDTO(entity);
            }


        }
    }
}
