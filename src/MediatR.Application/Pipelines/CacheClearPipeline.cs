using MediatR.Application.Constants;
using MediatR.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Pipelines
{
    /// <summary>
    /// Clears cache on item update.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class CacheClearPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                                           where TRequest : ICacheClear
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheClearPipeline<TRequest, TResponse>> _logger;

        public CacheClearPipeline(IMemoryCache cache,
                                  ILogger<CacheClearPipeline<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().ToString().Split(".").Last();

            if (_cache.TryGetValue(CacheKeys.GetItemsList, out _))
            {
                _cache.Remove(CacheKeys.GetItemsList);

                _logger.LogInformation($"{requestName} clear the list in cache");
            }

            // If we change an existing element, we remove it from the cache.
            // (Does not apply to the method 'CreatingItem',
            // because existing elements are not changed in any way)
            if (request.ItemId != null)
            {
                var cacheKey = CacheKeys.GetItemById + request.ItemId;

                if (_cache.TryGetValue(cacheKey, out _))
                {
                    _cache.Remove(cacheKey);
                }

                _logger.LogInformation($"{requestName} clear the cache item with key: {cacheKey}");
            }

            return await next();
        }
    }
}
