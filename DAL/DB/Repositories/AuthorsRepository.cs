using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookArchive
{
    public class AuthorsRepository : GenericEFRepository<Author>, IAuthorsRepository
    {
        private readonly BookArchiveDataContext dbx;
        private readonly IBookArchiveUOW uow;

        public AuthorsRepository(BookArchiveDataContext dataContext, IBookArchiveUOW uow) : base(dataContext)
        {
            this.dbx = dataContext;
            this.uow = uow;
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

        public override void Update(Author entity, IDbTransaction t = default)
        {
            var willCreateT = t == default;
            try
            {
                if (willCreateT)
                    t = uow.CreateTransaction();

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