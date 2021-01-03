
using AutoMapper;

namespace BookArchive.Application.CQRS
{
    public class AuthorUpdateDTO : AuthorAddDTO
    {
        public int Id { get; set; }

    }

    public class AuthorUpdateMap:Profile
    {
        public AuthorUpdateMap()
        {
            CreateMap<AuthorUpdateDTO, Author>()
                .ReverseMap();
        }
    }
}
