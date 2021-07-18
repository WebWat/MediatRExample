using FluentValidation;
using MediatR.Application.Entities;
using MediatR.Application.Interfaces;
using MediatR.Application.Pipelines;
using MediatR.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Web
{
    public static class Configure
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Item).Assembly);

            services.AddValidatorsFromAssembly(typeof(Item).Assembly);

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EFCoreRepository<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestTimePipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheClearPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingPipeline<,>));
        }
    }
}
