using MediatR.Application.Entities;
using MediatR.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Queries.GetItemsList
{
    public partial class GetItemsList
    {
        public class QueryHandler : IRequestHandler<Query, IEnumerable<Item>>
        {
            private readonly IAsyncRepository<Item> _repository;

            public QueryHandler(IAsyncRepository<Item> repository)
            {
                _repository = repository;
            }


            public async Task<IEnumerable<Item>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _repository.GetListAsync();

                return items;
            }
        }
    }
}
