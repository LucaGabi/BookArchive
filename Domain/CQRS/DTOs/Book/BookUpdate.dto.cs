using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookArchive.Application.CQRS
{
    public class BookUpdateDTO : BookAddDTO
    {
        public int Id { get; set; }
        public bool ClearImage { get; set; }
    }

    public static class BookUpdateMap
    {
        public static BookUpdateDTO ToDTO(this Book model, bool withLinks = true)
        {
            return new BookUpdateDTO
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CoverImagePath = model.CoverImagePath,
                BookAuthors = model.Authors?.Select(x => new BookAuthorGetDTO { AuthorId = x.Id, BookId = model.Id }).ToArray()
            };
        }

        public static Book ToModel(this BookUpdateDTO dto)
        {
            return new Book
            {
                Id=dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                AuthorBooks = dto.BookAuthors?.Select(x => x.ToModel()).ToArray(),
                CoverImagePath = dto.CoverImagePath,
            };
        }
    }
}
