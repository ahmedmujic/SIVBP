using Dapper;
using Microsoft.EntityFrameworkCore;
using SIVBP_Keširanje.Constants;
using SIVBP_Keširanje.DTOs;
using SIVBP_Keširanje.Models;
using SIVBP_Keširanje.Services;
using StackExchange.Redis;

namespace SIVBP_Keširanje.Subscribers
{
    public class UserEfficiencySubscriber : BackgroundService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger<UserEfficiencySubscriber> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserEfficiencySubscriber(IConnectionMultiplexer connectionMultiplexer,
            ILogger<UserEfficiencySubscriber> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber(stoppingToken);

            await subscriber.SubscribeAsync(RedisPubSubChannels.CommentAccepted, async (channel, value) =>
            {
                _logger.LogInformation("Processing event {topic}", channel);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<SIVBPContext>();
                    var cacheRepository = scope.ServiceProvider.GetRequiredService<ICacheRepository>();

                    var dbConntection = dbContext.Database.GetDbConnection();
                    var result = await Task.Run<IEnumerable<UserEfficiencyResponse>>(() => dbConntection.QueryAsync<UserEfficiencyResponse>(UserQueries.UserEfficiency, commandTimeout: 540));
                    await Task.Run(() => cacheRepository.SetByKeyAsync(RedisKeys.UserEfficiency, result, stoppingToken));
                }
            });
        }
    }
}
