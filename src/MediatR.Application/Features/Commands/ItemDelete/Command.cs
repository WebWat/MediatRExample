using MediatR.Application.Interfaces;

namespace MediatR.Application.Features.Commands.ItemDelete
{
    public partial class ItemDelete
    {
        public record Command(int Id) : IRequest, ICacheClear
        {
            public int? ItemId => Id;
        }
    }
}
