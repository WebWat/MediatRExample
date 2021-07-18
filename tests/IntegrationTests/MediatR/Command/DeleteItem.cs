using MediatR;
using MediatR.Application.Data;
using MediatR.Application.Entities;
using MediatR.Application.Features.Commands.ItemDelete;
using MediatR.Application.Interfaces;
using MediatR.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.MediatR.Command
{
    [Collection("Sequential")]
    public class DeleteItem
    {
        private readonly Mock<IMediator> _mockMediatR;
        private readonly IAsyncRepository<Item> _repository;
        private readonly ApplicationContext _context;

        public DeleteItem()
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
        public async Task DeleteItemWithNoneReturn()
        {
            // Arrange
            var item = await _context.Items.AsNoTracking().FirstAsync();
            var query = new ItemDelete.Command(item.Id);
            var handler = new ItemDelete.CommandHandler(_mockMediatR.Object, _repository);

            // Act
            await handler.Handle(query, default);

            // Assert
            Assert.Null(await _context.Items.FirstOrDefaultAsync(i => i.Id == item.Id));
        }
    }
}
