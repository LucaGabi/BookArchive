using Microsoft.EntityFrameworkCore;

namespace BookArchive
{
    public partial class BookArchiveDataContext
    {
        public DbSet<Book> Books { get; set; }
    }
}