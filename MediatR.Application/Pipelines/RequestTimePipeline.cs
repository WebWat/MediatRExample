using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Pipelines
{
    public class RequestTimePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<RequestTimePipeline<TRequest, TResponse>> _logger;

        public RequestTimePipeline(ILogger<RequestTimePipeline<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().ToString().Split(".").Last();

            _logger.LogInformation($"Starting request {requestName}");

            var stopwatch = Stopwatch.StartNew();

            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation($"{requestName} finished in {stopwatch.ElapsedMilliseconds / 1000d:f2} s");

            return response;
        }
    }
}
