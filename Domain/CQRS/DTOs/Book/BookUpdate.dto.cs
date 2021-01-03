using AutoMapper;

namespace BookArchive.Application.CQRS
{
    public class BookUpdateDTO : BookAddDTO
    {
        public int Id { get; set; }
        public bool ClearImage { get; set; }
    }

    public class BookUpdateMap : Profile
    {
        public BookUpdateMap()
        {
            CreateMap<BookUpdateDTO, Book>()
                .ReverseMap();
        }
    }
}
