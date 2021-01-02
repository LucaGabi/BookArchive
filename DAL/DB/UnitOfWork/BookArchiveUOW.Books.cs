

namespace BookArchive.DAL
{
    public partial class BookArchiveUnitOfWork : IUnitOfWork
    {
        public IBooksRepository BooksRepository
        {
            get
            {
                return (IBooksRepository)serviceProvider.GetService(typeof(IBooksRepository));
            }
        }
    }
}