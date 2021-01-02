using BookArchive.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookArchive.DAL
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(s => s.Id);
            builder.AddTimeTrackDefaults();
        }
    }
}
