namespace MediatR.Application.Features.Commands.ItemUpdate.Notifications
{
    public class ItemUpdateNotification : INotification
    {
        public int Id { get; }

        public ItemUpdateNotification(int id) => Id = id;
    }
}
