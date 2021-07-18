namespace MediatR.Application.Features.Commands.ItemCreate.Notifications
{
    public class ItemCreateNotification : INotification
    {
        public int Id { get; }

        public ItemCreateNotification(int id) => Id = id;
    }
}
