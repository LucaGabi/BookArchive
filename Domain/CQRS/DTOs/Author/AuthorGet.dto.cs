using AutoMapper;
using BookArchive.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookArchive.Application.CQRS
{
    public class AuthorGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookAuthorGetDTO> AuthorBooks { get; set; }
        public virtual ICollection<BookGetDTO> Books { get; set; }
    }

    public class AuthorGetMap : Profile
    {
        public AuthorGetMap()
        {
            CreateMap<AuthorGetDTO, Author>()
                .ReverseMap();
        }
    }
}
