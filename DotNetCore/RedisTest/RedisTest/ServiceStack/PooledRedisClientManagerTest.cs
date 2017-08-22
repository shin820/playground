using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;

namespace RedisTest.ServiceStack
{
    /// <summary>
    /// If you prefer to define options on the Client Manager itself 
    /// or you want to provide separate Read/Write and ReadOnly 
    /// (i.e. Master and Slave) redis-servers, 
    /// use the PooledRedisClientManager instead.
    /// </summary>
    public class PooledRedisClientManagerTest : RedisClientManagerTest
    {
        protected override IRedisClient GetRedisClient()
        {
            //The PooledRedisClientManager imposes a maximum
            //connection limit and when its maximum pool size
            //has been reached will instead block on any new
            //connection requests until the next RedisClient is 
            //released back into the pool. If no client became 
            //available within PoolTimeout, 
            //a PoolTimeoutException will be thrown.
            var readWriteHosts = new[] { "localhost:6379" };
            var readOnlyHosts = new[] { "localhost:6379" };
            var redisManager = new PooledRedisClientManager(readWriteHosts, readOnlyHosts);
            return redisManager.GetClient();
        }
    }
}
