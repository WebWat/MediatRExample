namespace MediatR.Application.Features.Commands.ItemDelete.Notifications
{
    public class ItemDeleteNotification : INotification
    {
        public int Id { get; }

        public ItemDeleteNotification(int id) => Id = id;
    }
}
