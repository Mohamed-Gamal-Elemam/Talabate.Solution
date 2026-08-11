using System;
using System.Collections.Generic;
using System.Text;

namespace Talabate.Core.IServices
{
    public interface IResponseCachServices
    {
        Task CachResponseAsync(string Key, object Responce, TimeSpan TimeToLife);

        Task<string?> GetCachResponseAsync(string Key);
    }
}
