// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;
ConnectionMultiplexer redis  = await ConnectionMultiplexer.ConnectAsync("192.168.136.130:8081");
IDatabase database = redis.GetDatabase(0);
await database.StringSetAsync("key1", "阿斯达实打实的");