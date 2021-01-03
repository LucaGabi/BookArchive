
namespace BookArchive
{
    public partial interface IBookArchiveUOW 
    {
        public IBooksRepository BooksRepository { get;  }
    }
}