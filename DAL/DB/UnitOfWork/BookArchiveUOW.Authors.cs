

namespace BookArchive
{
    public partial class BookArchiveUnitOfWork
    {
        public IAuthorsRepository AuthorsRepository
        {
            get
            {
                return (IAuthorsRepository)serviceProvider.GetService(typeof(IAuthorsRepository));
            }
        }
    }
}