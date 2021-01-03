using System;
using System.Collections.Generic;
using System.Linq;

namespace BookArchive
{
    public class Author : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}