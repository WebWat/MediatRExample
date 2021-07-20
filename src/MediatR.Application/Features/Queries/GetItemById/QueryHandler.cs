using MediatR.Application.Entities;
using MediatR.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Queries.GetItemById
{
    public partial class GetItemById
    {
        public class QueryHandler : IRequestHandler<Query, Item>
        {
            private readonly IAsyncRepository<Item> _repository;

            public QueryHandler(IAsyncRepository<Item> repository)
            {
                _repository = repository;
            }


            public async Task<Item> Handle(Query request, CancellationToken cancellationToken)
            {
                var item = await _repository.FindByIdAsync(request.Id, cancellationToken);

                return item;
            }
        }
    }
}
