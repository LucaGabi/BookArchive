
using BookArchive.DAL.Models;
using System.Linq;

namespace BookArchive.Application.CQRS
{
    public class AuthorUpdateDTO : AuthorAddDTO
    {
        public int Id { get; set; }

    }

    public static class AuthorUpdateMap
    {
        public static AuthorUpdateDTO ToDTO(this Author model, bool withLinks = true)
        {
            return new AuthorUpdateDTO
            {
                Id = model.Id,
                Name=model.Name,
                AuthorBooks = model.Books?.Select(x => new BookAuthorGetDTO { BookId = x.Id, AuthorId = model.Id }).ToArray()
            };
        }

        public static Author ToModel(this AuthorUpdateDTO dto)
        {
            return new Author
            {
                Id=dto.Id,
                Name= dto.Name,
                AuthorBooks=dto.AuthorBooks?.Select(x=>x.ToModel()).ToArray()
            };
        }
    }
}
