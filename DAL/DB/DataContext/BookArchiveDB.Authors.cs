using Microsoft.EntityFrameworkCore;


namespace BookArchive
{
    public partial class BookArchiveDataContext
    {
        public DbSet<Author> Authors { get; set; }
    }
}