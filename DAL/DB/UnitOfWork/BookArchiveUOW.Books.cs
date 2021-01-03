

namespace BookArchive
{
    public partial class BookArchiveUnitOfWork
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