using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Commands.ItemUpdate.Notifications
{
    public class ItemUpdateNotificationHandler : INotificationHandler<ItemUpdateNotification>
    {
        private readonly ILogger<ItemUpdateNotificationHandler> _logger;

        public ItemUpdateNotificationHandler(ILogger<ItemUpdateNotificationHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ItemUpdateNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Item {notification.Id} was updated");

            return Task.CompletedTask;
        }
    }
}
