using System;
using System.Threading.Tasks;

namespace PSE.Core.Data
{
    public interface IUnityOfWork
    {
        Task<bool> Commit();
    }
}
