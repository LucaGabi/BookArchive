using BookArchive.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace BookArchive.DAL
{
    public partial class BookArchiveDataContext
    {
        public DbSet<Author> Authors { get; set; }
    }
}