
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BookArchive
{
    internal static class X
    {
        public static EntityTypeBuilder<T> AddTimeTrackDefaults<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.Property(x => x.CreatedOn)
                .ValueGeneratedOnAdd()
                .HasDefaultValue(DateTime.Now);
            builder.Property(x => x.UpdatedOn)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValue(DateTime.Now);
            return builder;
        }

    }
}
