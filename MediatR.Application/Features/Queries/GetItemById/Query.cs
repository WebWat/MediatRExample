using MediatR.Application.Constants;
using MediatR.Application.Entities;
using MediatR.Application.Interfaces;

namespace MediatR.Application.Features.Queries.GetItemById
{
    public partial class GetItemById
    {
        public record Query(int Id) : IRequest<Item>, ICached
        {
            public string CacheKey => CacheKeys.GetItemById + Id;
        }
    }
}
