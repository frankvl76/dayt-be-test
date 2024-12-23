namespace Dayt.EF.Tests
{
    using Dayt.EF.Data;
    using Dayt.EF.Logic;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    public class EventRepositoryTests
    {
        private readonly EventDbContext _context;
        private readonly EventRepository _repository;

        public EventRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EventDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new EventDbContext(options);
            _repository = new EventRepository(_context);
        }

        [Fact]
        public async Task SaveEvent_ShouldSaveAndRetrieveEventCorrectly()
        {
            // Arrange
            var eventModel = new EventModel
            {
                Title = "Dance event 1",
                Description = "This is a test event.",
                City = "Berlin"
            };

            // Act
            await _repository.SaveEventAsync(eventModel);
            var retrievedEvent = await _repository.GetEventAsync(eventModel.Id);

            // Assert
            retrievedEvent.ShouldNotBeNull();            
            eventModel.Title.ShouldBe(retrievedEvent.Title);
            eventModel.Description.ShouldBe(retrievedEvent.Description);
            eventModel.City.ShouldBe(retrievedEvent.City);
        }
    }
}