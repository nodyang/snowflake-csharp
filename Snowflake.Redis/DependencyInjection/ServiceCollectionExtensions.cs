﻿using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using Snowflake.Redis.Cache;
using System;

namespace Snowflake.Redis.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSnowflakeRedisService(this IServiceCollection services, string connectionString,Action<SnowflakeOptions> action)
        {
            RedisHelper.Initialization(new CSRedisClient(connectionString));
            SnowflakeOptions snowflakeOptions=new SnowflakeOptions();
            action.Invoke(snowflakeOptions);
            var machineIdConfig = new MachineIdConfig(new RedisCacheAsync(), snowflakeOptions);
            services.AddSingleton(typeof(SnowFlake), machineIdConfig.InitMachineId().Result);
            return services;
        }
    }
}
