using MediatR.Application.Interfaces;

namespace MediatR.Application.Features.Commands.ItemCreate
{
    public partial class ItemCreate
    {
        public record Command(string Title, double UniqueNumber) : IRequest<int>, ICacheClear
        {
            public int? ItemId { get; }
        }
    }
}
