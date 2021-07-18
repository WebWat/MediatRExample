using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemCreate.Notifications;
using MediatR.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Commands.ItemCreate
{
    public partial class ItemCreate
    {
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly IMediator _mediator;
            private readonly IAsyncRepository<Item> _repository;

            public CommandHandler(IMediator mediator, IAsyncRepository<Item> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }


            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = new Item { Title = request.Title, UniqueNumber = request.UniqueNumber };

                await _repository.CreateAsync(item);

                await _mediator.Publish(new ItemCreateNotification(item.Id));

                return item.Id;
            }
        }
    }
}
