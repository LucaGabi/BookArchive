
namespace BookArchive
{
    public interface IBooksRepository: IRepository<Book>
    {
        Book GetById(int id);
    }
}