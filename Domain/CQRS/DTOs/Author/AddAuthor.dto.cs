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

    public static class AuthorAddMap
    {
        public static AuthorAddDTO ToDTO(this Author model)
        {
            return new AuthorAddDTO
            {
                Name = model.Name,
                AuthorBooks = model.AuthorBooks?.Select(x => BookAuthorGetMap.ToDTO(x)).ToArray()
            };
        }

        public static Author ToModel(this AuthorAddDTO dto)
        {
            return new Author
            {
                Name = dto.Name,
                AuthorBooks = dto.AuthorBooks?.Select(x=>BookAuthorGetMap.ToModel(x)).ToArray()
            };
        }
    }
}