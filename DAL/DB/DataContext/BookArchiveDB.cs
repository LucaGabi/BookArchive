using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookArchive
{
    public partial class BookArchiveDataContext : DbContext, IUnitOfWork
    {
        public BookArchiveDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public IDbTransaction CreateTransaction()
        {
            return new EFDbTransaction(this);
        }

        public async Task<int> Save(CancellationToken cancelationToken = default)
        {
            return await base.SaveChangesAsync(cancelationToken);
        }
    }
}