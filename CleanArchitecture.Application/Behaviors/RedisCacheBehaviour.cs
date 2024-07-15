﻿using CleanArchitecture.Application.Interfaces.RedisCache;
using MediatR;

namespace CleanArchitecture.Application.Behaviors
{
    public class RedisCacheBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IRedisCacheService _redisCacheService;
        public RedisCacheBehaviors(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICacheableQuery cacheableQuery)
            {
                var CacheKey = cacheableQuery.CacheKey;

                var CacheTime = cacheableQuery.CacheTime;

                var cacheData = await _redisCacheService.GetAsync<TResponse>(CacheKey);

                if (cacheData is not null)
                    return cacheData;

                var response = await next();

                if (response is not null)
                    await _redisCacheService.SetAsync(CacheKey, response, DateTime.UtcNow.AddMinutes(CacheTime));
                return response;

            }
            return await next();
        }
    }
}
