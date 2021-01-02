using System;

namespace BookArchive.DAL.Models
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.Now;
        }

        
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}