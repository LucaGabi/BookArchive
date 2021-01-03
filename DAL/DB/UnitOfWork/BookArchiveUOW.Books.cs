

namespace BookArchive
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