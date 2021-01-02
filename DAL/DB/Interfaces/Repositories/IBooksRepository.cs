using BookArchive.DAL.Models;

namespace BookArchive.DAL
{
    public interface IBooksRepository: IRepository<Book>
    {
        Book GetById(int id);
    }
}