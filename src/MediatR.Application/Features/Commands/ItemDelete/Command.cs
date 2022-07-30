using MediatR.Application.Interfaces;

namespace MediatR.Application.Features.Commands.ItemDelete
{
    public partial class ItemDelete
    {
        public record Command(int Id) : IRequest, ICacheClear<int>
        {
            public int? ItemId => Id;
        }
    }
}
