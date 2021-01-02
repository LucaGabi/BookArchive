
using AutoMapper;
using BookArchive.DAL.Models;
using System.Linq;

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
