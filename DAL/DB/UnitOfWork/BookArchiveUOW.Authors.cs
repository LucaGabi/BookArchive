﻿

namespace BookArchive.DAL
{
    public partial class BookArchiveUnitOfWork : IUnitOfWork
    {
        public IAuthorsRepository AuthorsRepository
        {
            get
            {
                return (IAuthorsRepository)serviceProvider.GetService(typeof(IAuthorsRepository));
            }
        }
    }
}