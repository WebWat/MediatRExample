using MediatR.Application.Entities;
using MediatR.Application.Features.Queries.GetItemsList;
using MediatR.Application.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.MediatR.Queries
{
    public class ReturnListOfItems
    {
        private readonly Random random = new();
        private readonly Mock<IAsyncRepository<Item>> _mockRepository;

        public ReturnListOfItems()
        {
            _mockRepository = new();
            _mockRepository.Setup(f => f.GetListAsync(default)).ReturnsAsync(GetItems());
        }


        [Fact]
        public async Task ReturnItems()
        {
            // Arrange
            var query = new GetItemsList.Query();
            var handler = new GetItemsList.QueryHandler(_mockRepository.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Equal(GetItems().Count, (result as List<Item>).Count);
        }


        private List<Item> GetItems()
        {
            return new()
            {
                new Item { Title = "Pencil", UniqueNumber = random.NextDouble() },
                new Item { Title = "Notebook", UniqueNumber = random.NextDouble() },
                new Item { Title = "Monitor", UniqueNumber = random.NextDouble() },
                new Item { Title = "Phone", UniqueNumber = random.NextDouble() },
                new Item { Title = "Keyboard", UniqueNumber = random.NextDouble() },
            };
        }
    }
}
