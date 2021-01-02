using BookArchive.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;

namespace BookArchive.DAL
{
    public class AuthorsRepository : GenericEFRepository<Author>, IAuthorsRepository
    {
        private readonly BookArchiveDataContext dbx;

        public AuthorsRepository(BookArchiveDataContext dataContext, IBookArchiveUOW uow) : base(dataContext)
        {
            this.dbx = dataContext;
        }

        public new IEnumerable<Author> Get()
        {
            return dbx.Authors
                .Include(x => x.Books)
                .AsNoTracking();
        }

        public virtual Author GetById(int id)
        {
            return dbx.Authors
                .Include(x => x.AuthorBooks)
                .Include(x => x.Books)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public override void Update(Author entity, IDbContextTransaction t = default)
        {
            var willCreateT = t == default;
            try
            {
                if (willCreateT)
                    t = dbx.CreateTransaction();

                var dbo = dbx.Authors
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