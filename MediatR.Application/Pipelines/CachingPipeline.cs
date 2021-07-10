using MediatR.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Pipelines
{
    public class CachingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                                        where TRequest : ICached
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachingPipeline<TRequest, TResponse>> _logger;

        public CachingPipeline(IMemoryCache cache,
                               ILogger<CachingPipeline<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().ToString().Split(".").Last();

            _logger.LogInformation($"{requestName} is configured for caching");

            if (_cache.TryGetValue(request.CacheKey, out TResponse response))
            {
                _logger.LogInformation($"Returning cached value for {requestName}. Cache Key: {request.CacheKey}");

                return response;
            }

            _logger.LogInformation($"{requestName} returned not from the cache");

            response = await next();
            
            if (response == null)
            {
                _logger.LogWarning("Object for caching not found");

                return response;
            }
        
            _cache.Set(request.CacheKey, response);

            return response;
        }
    }
}
