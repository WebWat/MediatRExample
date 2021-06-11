using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemDelete.Notifications;
using MediatR.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Commands.ItemDelete
{
    public partial class ItemDelete
    {
        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly IMediator _mediator;
            private readonly IAsyncRepository<Item> _repository;

            public CommandHandler(IMediator mediator, IAsyncRepository<Item> repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _repository.FindByIdAsync(request.Id);

                if (item == null)
                {
                    throw new ArgumentNullException($"Item with id {request.Id} not exists");
                }

                await _repository.DeleteAsync(item);

                await _mediator.Publish(new ItemDeleteNotification(item.Id)); //TODO: ??

                return Unit.Value;
            }
        }
    }
}
