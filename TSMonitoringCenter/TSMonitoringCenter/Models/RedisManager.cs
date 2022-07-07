using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSMonitoringCenter.Models
{
    public class RedisManager
    {
        public static RedisManager instance;
        private ConnectionMultiplexer redis;
        private IDatabase db;

        private RedisManager()
        {
            redis = ConnectionMultiplexer.Connect(Configuration.GetInstance().RedisConnectionString);
            db = redis.GetDatabase(Convert.ToInt32(Configuration.GetInstance().RedisDBNumber));
        }
        public static RedisManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RedisManager();
            }
            return instance;
        }

        private bool KillRedisKey(string key)
        {
            if (db.KeyDelete("expiration:session:" + key) && db.KeyDelete("session:" + key)
                && db.KeyDelete(key + ":Data") && db.KeyDelete(key + ":Cache"))
            {
                return true;
            }


            return false;
        }

        public static int CloseSessions()
        {
            int count = 0;
            foreach (var item in DBUtils.GetInstance().GetSessionsToKill())
            {
                GetInstance().KillRedisKey(item[4]);
                DBUtils.GetInstance().UpdateSession(item[0]);
                count++;
            }
            return count;
        }


    }
}