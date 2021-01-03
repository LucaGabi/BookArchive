
using AutoMapper;
using System.Collections.Generic;

namespace BookArchive.Application.CQRS
{
    public class BookGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }


        public ICollection<BookAuthorGetDTO> BookAuthors { get; set; }
        public virtual ICollection<AuthorGetDTO> Authors { get; set; }
    }

    public class BookGetMap:Profile
    {
        public BookGetMap()
        {
            CreateMap<BookGetDTO, Book>()
                .ReverseMap();
        }
    }


}
