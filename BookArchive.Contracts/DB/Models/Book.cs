using System;
using System.Collections.Generic;

namespace BookArchive
{
    public class Book: BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }


        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();

    }

}