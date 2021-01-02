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

    public static class BookAddMap
    {
        public static BookAddDTO ToDTO(this Book model, bool withLinks = true)
        {
            return new BookAddDTO
            {
                Title = model.Title,
                Description = model.Description,
                CoverImagePath = model.CoverImagePath,
                BookAuthors = model.Authors?.Select(x => new BookAuthorGetDTO { AuthorId = x.Id, BookId = model.Id }).ToArray()
            };
        }

        public static Book ToModel(this BookAddDTO dto)
        {
            return new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                CoverImagePath = dto.CoverImagePath,
                AuthorBooks = dto.BookAuthors?.Select(x => x.ToModel()).ToArray(),
            };
        }
    }

}