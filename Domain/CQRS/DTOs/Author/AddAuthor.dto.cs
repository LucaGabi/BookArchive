using AutoMapper;
using BookArchive.DAL.Models;
using System.Collections.Generic;
using System.Linq;

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