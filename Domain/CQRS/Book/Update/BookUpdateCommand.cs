using BookArchive.DAL;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace BookArchive.Application.CQRS
{
    public class BookUpdateCommand : BookUpdateDTO, IRequest<CQRSResult<BookGetDTO>>
    {

        private Stream coverImage;

        public void SetCoverImage(Stream value)
        {
            coverImage = value;
        }

        public class BookUpdateCommandHandler : IRequestHandler<BookUpdateCommand, CQRSResult<BookGetDTO>>
        {
            private readonly IBookArchiveUOW uow;
            private readonly IFileStorageService fileStorageService;

            public BookUpdateCommandHandler(IBookArchiveUOW uow, IFileStorageService fileStorageService)
            {
                this.uow = uow;
                this.fileStorageService = fileStorageService;
            }

            public async Task<CQRSResult<BookGetDTO>> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
            {
                var exists = uow.BooksRepository.Contains(x => x.Id == request.Id);
                if (exists)
                {
                    if (request.ClearImage)
                    {
                        var coverImagePath = Path.GetFileName(request.CoverImagePath);
                        if(await fileStorageService.Delete(coverImagePath)) request.CoverImagePath="";
                    }
                    else if (request.coverImage.Length > 0)
                    {
                        var fExt = Path.GetExtension(request.CoverImagePath);
                        var fNewName = Guid.NewGuid().ToString()+fExt;
                        request.CoverImagePath = await fileStorageService.Save(request.coverImage, fNewName);
                    }

                    var entity = BookUpdateMap.ToModel(request);
                    uow.BooksRepository.Update(entity);
                    await uow.Save();
                    return BookGetMap.ToDTO(entity);
                }
                else
                    return (null as BookGetDTO).AsCQRSResult(code: 404);
            }

        }
    }
}
