using AutoMapper;
using System.Collections.Generic;

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
            CreateMap<BookAddDTO, Book>()
                .ReverseMap();
        }
    }
}