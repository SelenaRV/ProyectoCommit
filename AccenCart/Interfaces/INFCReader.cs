using System;
using System.Threading.Tasks;

namespace AccenCart.Interfaces
{
    public interface INFCReader
    {
        Task Start();
        Task<bool> IsNFCCapable();
        Task Stop();
        event EventHandler<string> TagReaded;
        event EventHandler<string> ReadingCancelled;
    }
}
