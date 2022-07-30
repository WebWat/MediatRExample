using MediatR.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Pipelines
{
    public interface ICacheClearPipeline<TRequest, TResponse> where TRequest : IRequest<ICacheClear<int>>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
    }
}