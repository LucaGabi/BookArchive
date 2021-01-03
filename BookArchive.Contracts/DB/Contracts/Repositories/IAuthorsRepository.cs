using System.Collections.Generic;

namespace BookArchive
{
    public interface IAuthorsRepository: IRepository<Author>
    {
        IEnumerable<Author> Get();
        Author GetById(int id);
    }
}