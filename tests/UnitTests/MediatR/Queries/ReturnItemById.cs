using MediatR.Application.Entities;
using MediatR.Application.Features.Queries.GetItemById;
using MediatR.Application.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.MediatR.Queries
{
    public class ReturnItemById
    {
        private readonly Mock<IAsyncRepository<Item>> _mockRepository;

        public ReturnItemById()
        {
            _mockRepository = new();
            _mockRepository.Setup(f => f.FindByIdAsync(1, default)).ReturnsAsync(GetItem());
        }


        [Fact]
        public async Task ReturnItem()
        {
            // Arrange
            var query = new GetItemById.Query(1);
            var handler = new GetItemById.QueryHandler(_mockRepository.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(GetItem().Equals(result));
        }


        private Item GetItem() =>
                new Item { Id = 1, Title = "Pencil", UniqueNumber = 0.12d };
    }
}
