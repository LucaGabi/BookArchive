using BookArchive.DAL.Models;
using System.Collections.Generic;

namespace BookArchive.DAL
{
    public interface IAuthorsRepository: IRepository<Author>
    {
        IEnumerable<Author> Get();
        Author GetById(int id);
    }
}