using MediatR;
using MediatR.Application.Data;
using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemCreate;
using MediatR.Application.Interfaces;
using MediatR.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.MediatR.Command
{
    [Collection("Sequential")]
    public class CreateItem
    {
        private readonly Mock<IMediator> _mockMediatR;
        private readonly IAsyncRepository<Item> _repository;
        private readonly ApplicationContext _context;

        public CreateItem()
        {
            _mockMediatR = new();

            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "MediatR1")
                .Options;
            _context = new ApplicationContext(dbOptions);
            _repository = new EFCoreRepository<Item>(_context);         
        }


        [Fact]
        public async Task CreateItemAndReturnId()
        {
            // Arrange
            var query = new ItemCreate.Command("Laptop", 0.12d);
            var handler = new ItemCreate.CommandHandler(_mockMediatR.Object, _repository);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Equal((await _context.Items.AsNoTracking().LastAsync()).Id, result);
        }
    }
}
