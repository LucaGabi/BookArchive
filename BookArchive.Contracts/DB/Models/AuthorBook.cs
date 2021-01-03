using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookArchive
{
    public class AuthorBook:BaseEntity {
        public int BookId { get; set; }   
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}