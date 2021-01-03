using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookArchive
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Books)
                   .WithMany(x => x.Authors)
                   .UsingEntity<AuthorBook>(
                        j => j.HasOne(b => b.Book).WithMany(a => a.AuthorBooks),
                        j => j.HasOne(a => a.Author).WithMany(b => b.AuthorBooks)
                   );

            builder.AddTimeTrackDefaults();
        }
    }
}
