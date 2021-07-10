using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Features.Commands.ItemDelete.Notifications
{
    public class ItemDeleteNotificationHandler : INotificationHandler<ItemDeleteNotification>
    {
        private readonly ILogger<ItemDeleteNotificationHandler> _logger;

        public ItemDeleteNotificationHandler(ILogger<ItemDeleteNotificationHandler> logger)
        {
            _logger = logger;
        }


        public Task Handle(ItemDeleteNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Item {notification.Id} was deleted");

            return Task.CompletedTask;
        }
    }
}
