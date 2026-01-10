using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interface
{
    public interface IDatabaseTransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
