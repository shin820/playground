using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RedisTest.ServiceStack
{
    public class RedisManagerPoolTest : RedisClientManagerTest
    {
        protected override IRedisClient GetRedisClient()
        {
            //Any connections required after the maximum Pool size has been
            //reached will be created and disposed outside of the Pool. 
            //By not being restricted to a maximum pool size, the pooling
            //behavior in RedisManagerPool can maintain a smaller connection
            //pool size at the cost of potentially having a higher 
            //opened / closed connection count.
            var redisManager = new RedisManagerPool("localhost:6379");
            return redisManager.GetClient();
        }
    }
}
