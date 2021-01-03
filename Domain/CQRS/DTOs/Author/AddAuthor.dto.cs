using AutoMapper;
using System.Collections.Generic;

namespace BookArchive.Application.CQRS
{
    public class AuthorAddDTO
    {
        public string Name { get; set; }
        public ICollection<BookAuthorGetDTO> AuthorBooks { get; set; }
    }

    public class AuthorAddMap: Profile
    {
        public AuthorAddMap() 
        {
            CreateMap<AuthorAddDTO, Author>()
                .ReverseMap();
        }
    }
}