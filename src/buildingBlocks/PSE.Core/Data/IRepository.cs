using PSE.Core.DomainObjects;
using System;

namespace PSE.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregatedRoot
{
    IUnityOfWork UnitOfWork { get; }
}