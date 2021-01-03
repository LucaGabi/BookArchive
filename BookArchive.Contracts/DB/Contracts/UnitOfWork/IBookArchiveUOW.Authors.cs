
namespace BookArchive
{
    public partial interface IBookArchiveUOW 
    {
        IAuthorsRepository AuthorsRepository { get; }
    }
}