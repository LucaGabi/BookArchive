
namespace BookArchive.DAL
{
    public partial interface IBookArchiveUOW 
    {
        public IBooksRepository BooksRepository { get;  }
    }
}