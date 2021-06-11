namespace MediatR.Application.Interfaces
{
    public interface ICached
    {
        string CacheKey { get; }
    }
}
