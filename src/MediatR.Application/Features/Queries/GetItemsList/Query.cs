using MediatR.Application.Constants;
using MediatR.Application.Entities;
using MediatR.Application.Interfaces;
using System.Collections.Generic;

namespace MediatR.Application.Features.Queries.GetItemsList
{
    public partial class GetItemsList
    {
        public record Query : IRequest<IEnumerable<Item>>, ICached<int>
        {
            public string CacheKey => CacheKeys.GetItemsList;
        }
    }
}
