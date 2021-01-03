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

    public static class AuthorGetMap
    {
        public static AuthorGetDTO ToDTO(this Author model, bool withLinks = true)
        {
            return new AuthorGetDTO
            {
                Id = model.Id,
                Name = model.Name,
                AuthorBooks = model.AuthorBooks?.Select(x => BookAuthorGetMap.ToDTO(x)).ToArray(),
                Books = withLinks
                        ? model.Books?.Select(x => BookGetMap.ToDTO(x, false)).ToArray()
                        : new BookGetDTO[0]
            };
        }

        public static Author ToModel(this AuthorGetDTO dto)
        {
            return new Author
            {
                Id = dto.Id,
                Name = dto.Name,
                AuthorBooks = dto.Books?.Select(x => new AuthorBook { AuthorId = dto.Id, BookId = x.Id }).ToArray(),
                Books = null
            };
        }
    }
}
