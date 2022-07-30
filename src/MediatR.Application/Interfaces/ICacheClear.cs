namespace MediatR.Application.Interfaces
{
    public interface ICacheClear<T> : IRequest<T> 
    {
        int? ItemId { get; }
    }
}
