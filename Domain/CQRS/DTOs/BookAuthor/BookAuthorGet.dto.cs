namespace BookArchive.Application.CQRS
{
    public class BookAuthorGetDTO
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

    }

    public static class BookAuthorGetMap
    {
        static public AuthorBook ToModel(this BookAuthorGetDTO link)
        {
            return new AuthorBook { AuthorId = link.AuthorId, BookId = link.BookId };
        }

        public static BookAuthorGetDTO ToDTO(this AuthorBook link)
        {
            return new BookAuthorGetDTO { AuthorId = link.AuthorId, BookId = link.BookId };
        }
    }
}
