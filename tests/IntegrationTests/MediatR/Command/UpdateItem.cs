using MediatR;
using MediatR.Application.Data;
using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemUpdate;
using MediatR.Application.Interfaces;
using MediatR.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.MediatR.Command
{
    [Collection("Sequential")]
    public class UpdateItem
    {
        private readonly Mock<IMediator> _mockMediatR;
        private readonly IAsyncRepository<Item> _repository;
        private readonly ApplicationContext _context;

        public UpdateItem()
        {
            _mockMediatR = new();

            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "MediatR1")
                .Options;
            _context = new ApplicationContext(dbOptions);
            _repository = new EFCoreRepository<Item>(_context);

            ApplicationContextSeed.SeedAsync(_context).Wait();

            _context.ChangeTracker.Clear();
        }


        [Fact]
        public async Task UpdateItemAndReturnId()
        {
            // Arrange
            var item = await _context.Items.AsNoTracking().FirstAsync();
            var query = new ItemUpdate.Command(item.Id, "Laptop", item.UniqueNumber);
            var handler = new ItemUpdate.CommandHandler(_mockMediatR.Object, _repository);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Equal(item.Id, result);
        }
    }
}
