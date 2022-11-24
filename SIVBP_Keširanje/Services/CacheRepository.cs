using StackExchange.Redis;
using Newtonsoft.Json;

namespace SIVBP_Keširanje.Services
{
    public interface ICacheRepository
    {
        Task SetByKeyAsync<T>(string key, T value, CancellationToken cancellationToken = default);
        Task<T?> GetByKeyAsync<T>(string key, CancellationToken cancellationToken = default);
    }

    public class CacheRepository : ICacheRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task SetByKeyAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<T?> GetByKeyAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var value = await db.StringGetAsync(key);
            if (value.HasValue)
            {
                return JsonConvert.DeserializeObject<T?>(value);
            }

            return default;
        }
    }
}
