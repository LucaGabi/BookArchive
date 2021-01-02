using AutoMapper;
using BookArchive.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            CreateMap<BookAuthorGetDTO, Book>()
                .ReverseMap();
        }
    }
}
