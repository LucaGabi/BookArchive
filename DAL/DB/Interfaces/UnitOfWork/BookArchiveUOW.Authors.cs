
namespace BookArchive.DAL
{
    public partial interface IBookArchiveUOW 
    {
        IAuthorsRepository AuthorsRepository { get; }
    }
}