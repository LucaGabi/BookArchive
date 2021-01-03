using System;

namespace BookArchive
{
    public interface IDbTransaction: IDisposable
    {
        void Commit();
        void Rollback();
    }
}
