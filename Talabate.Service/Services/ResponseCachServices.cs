using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Talabate.Core.IServices;

namespace Talabate.Application.Services
{
    public class ResponseCachServices : IResponseCachServices
    {
        private readonly IDatabase _database;

        public ResponseCachServices(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task CachResponseAsync(string Key, object Responce, TimeSpan TimeToLife)
        {
            if (Responce is null) return;

            var servilzeOptions=new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var SerializeResponse=JsonSerializer.Serialize(Responce, servilzeOptions);

            await _database.StringSetAsync(Key, SerializeResponse, TimeToLife);
            
        }

        public async Task<string?> GetCachResponseAsync(string Key)
        {
            var response=await _database.StringGetAsync(Key);
            if(response.IsNullOrEmpty)return null;
            return response;
        }
    }
}
