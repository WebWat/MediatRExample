using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Commands.ItemCreate.Notifications
{
    public class ItemCreateNotificationHandler : INotificationHandler<ItemCreateNotification>
    {
        private readonly ILogger<ItemCreateNotificationHandler> _logger;

        public ItemCreateNotificationHandler(ILogger<ItemCreateNotificationHandler> logger)
        {
            _logger = logger;
        }


        public Task Handle(ItemCreateNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Item {notification.Id} was created");

            return Task.CompletedTask;
        }
    }
}
