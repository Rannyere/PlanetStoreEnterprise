using System;
using PSE.Core.DomainObjects;

namespace PSE.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregatedRoot
    {
        IUnityOfWork UnitOfWork { get; }
    }
}
