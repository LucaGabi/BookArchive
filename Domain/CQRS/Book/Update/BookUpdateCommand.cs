using AutoMapper;
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
            private readonly IMapper mapper;

            public BookUpdateCommandHandler(IBookArchiveUOW uow, IFileStorageService fileStorageService, IMapper mapper)
            {
                this.uow = uow;
                this.fileStorageService = fileStorageService;
                this.mapper = mapper;
            }

            public async Task<CQRSResult<BookGetDTO>> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
            {
                var exists = uow.BooksRepository.Contains(x => x.Id == request.Id);
                if (exists)
                {
                    if (request.ClearImage)
                    {
                        var coverImagePath = Path.GetFileName(request.CoverImagePath);
                        if (await fileStorageService.Delete(coverImagePath)) request.CoverImagePath = "";
                    }
                    else if (request.coverImage.Length > 0)
                    {
                        var fExt = Path.GetExtension(request.CoverImagePath);
                        var fNewName = Guid.NewGuid().ToString() + fExt;
                        request.CoverImagePath = await fileStorageService.Save(request.coverImage, fNewName);
                    }

                    var entity = mapper.Map<Book>(request);
                    uow.BooksRepository.Update(entity);
                    await uow.Save();
                    return mapper.Map<BookGetDTO>(entity);
                }
                else
                    return mapper.Map<BookGetDTO>(request).AsResult(code: 404);
            }

        }
    }
}
