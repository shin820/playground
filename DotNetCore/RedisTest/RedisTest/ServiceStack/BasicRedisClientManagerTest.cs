using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;

namespace RedisTest.ServiceStack
{
    /// <summary>
    /// If don't want to use connection pooling (i.e. your 
    /// accessing a local redis-server instance) you can use 
    /// a basic (non-pooled) Clients Manager which 
    /// creates a new RedisClient instance each time:
    /// </summary>
    public class BasicRedisClientManagerTest : RedisClientManagerTest
    {
        protected override IRedisClient GetRedisClient()
        {
            var redisManager = new BasicRedisClientManager("localhost:6379");
            return redisManager.GetClient();
        }
    }
}
