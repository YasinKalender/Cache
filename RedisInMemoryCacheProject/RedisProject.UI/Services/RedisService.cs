using StackExchange.Redis;

namespace RedisProject.UI.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }

        public void Connect()
        {
            var configHostPort = $"{_redisHost}:{_redisPort}";

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configHostPort);
        }

        public IDatabase GetDatabse(int db)
        {
            return _connectionMultiplexer.GetDatabase(db);
        }
    }
}
