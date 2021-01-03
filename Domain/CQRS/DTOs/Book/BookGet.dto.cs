
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

    public static class BookGetMap
    {
        public static BookGetDTO ToDTO(this Book model, bool withLinks = true)
        {
            return new BookGetDTO
            {
                Id = model.Id,
                Title = model.Title,
                CoverImagePath = model.CoverImagePath,
                Description = model.Description,
                BookAuthors = model.AuthorBooks?.Select(x=>BookAuthorGetMap.ToDTO(x)).ToArray(),
                Authors = withLinks
                            ? model.Authors?.Select(x => AuthorGetMap.ToDTO(x,false)).ToArray()
                            : new AuthorGetDTO[0]
            };
        }

        public static Book ToModel(this BookGetDTO dto)
        {
            return new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CoverImagePath = dto.CoverImagePath,
                Authors = dto.Authors?.Select(x => AuthorGetMap.ToModel(x)).ToArray()
            };
        }
    }
}
