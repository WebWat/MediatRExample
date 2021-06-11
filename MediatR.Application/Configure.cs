using FluentValidation;
using MediatR.Application.Interfaces;
using MediatR.Application.Pipelines;
using MediatR.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Application
{
    public static class Configure
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Configure).Assembly);

            services.AddValidatorsFromAssembly(typeof(Configure).Assembly);

            services.AddMemoryCache();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EFCoreRepository<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestTimePipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheClearPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingPipeline<,>));
        }
    }
}
