namespace MediatR.Application.Interfaces
{
    public interface ICached<T> : IRequest<T>
    {
        string CacheKey { get; }
    }
}
