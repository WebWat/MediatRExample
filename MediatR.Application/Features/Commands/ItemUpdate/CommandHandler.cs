using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemUpdate.Notifications;
using MediatR.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Commands.ItemUpdate
{
    public partial class ItemUpdate
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
                if (await _repository.FindByIdAsync(request.Id) == null)
                {
                    throw new ArgumentNullException($"Item with id {request.Id} not exists");
                }

                var item = new Item { Id = request.Id, Title = request.Title, UniqueNumber = request.UniqueNumber };

                await _repository.UpdateAsync(item);

                await _mediator.Publish(new ItemUpdateNotification(item.Id));

                return request.Id;
            }
        }
    }
}
