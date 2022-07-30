using MediatR.Application.Interfaces;

namespace MediatR.Application.Features.Commands.ItemUpdate
{
    public partial class ItemUpdate
    {
        public record Command(int Id, string Title, double UniqueNumber) : IRequest<int>, ICacheClear<int>
        {
            public int? ItemId => Id; 
        }
    }
}
