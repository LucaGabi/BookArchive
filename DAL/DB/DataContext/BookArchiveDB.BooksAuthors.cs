using Microsoft.EntityFrameworkCore;

namespace BookArchive
{
    public partial class BookArchiveDataContext
    {
        public DbSet<AuthorBook> AuthorBook { get; set; }

    }
}