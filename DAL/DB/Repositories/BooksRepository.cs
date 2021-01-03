using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookArchive
{
    public class BooksRepository : GenericEFRepository<Book>, IBooksRepository
    {
        private readonly BookArchiveDataContext dbx;
        private readonly IBookArchiveUOW uow;

        public BooksRepository(BookArchiveDataContext dataContext, IBookArchiveUOW uow) : base(dataContext)
        {
            this.dbx = dataContext;
            this.uow = uow;
        }

        public new IEnumerable<Book> Get()
        {
            return dbx.Books
                .Include(x => x.AuthorBooks)
                .Include(x => x.Authors)
                .AsNoTracking();
        }

        public virtual Book GetById(int id)
        {
            return dbx.Books
                .Include(x=>x.AuthorBooks)
                .Include(x=>x.Authors)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Book entity, IDbTransaction t = default)
        {
            var willCreateT = t == default;
            try
            {
                if (willCreateT)
                    t = uow.CreateTransaction();

                var dbo = dbx.Books
                    .Include(x => x.AuthorBooks)
                    .AsNoTracking()
                    .First(x => x.Id == entity.Id);

                dbx.RemoveRange(dbo.AuthorBooks);

                CreateSavePoint(t);

                dbx.AddRange(entity.AuthorBooks);

                CreateSavePoint(t);

                dbx.Update(entity);
                dbx.SaveChanges();

                if (willCreateT)
                {
                    t.Commit();
                    t.Dispose();
                }
            }
            catch
            {
                t.Rollback();
                throw;
            }
            finally
            {
                if (willCreateT) t?.Dispose();
            }

        }


    }
}