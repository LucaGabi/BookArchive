using AutoMapper;
using BookArchive.DAL.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookArchive.Application.CQRS
{
    public class BookAddDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }


        public virtual ICollection<BookAuthorGetDTO> BookAuthors { get; set; }

    }

    public class BookAddMap : Profile
    {
        public BookAddMap()
        {
            CreateMap<BookAddDTO, Author>()
                .ReverseMap();
        }
    }
}