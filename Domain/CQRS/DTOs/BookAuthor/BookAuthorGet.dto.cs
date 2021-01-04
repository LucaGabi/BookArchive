using AutoMapper;

namespace BookArchive.Application.CQRS
{
    public class BookAuthorGetDTO
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }

    public class BookAuthorGetMap:Profile
    {
        public BookAuthorGetMap()
        {
            CreateMap<BookAuthorGetDTO, AuthorBook>()
                .ReverseMap();
        }
    }
}
