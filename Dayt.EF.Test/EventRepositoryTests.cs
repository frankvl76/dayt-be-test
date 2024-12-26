namespace Dayt.EF.Tests
{
    using Dayt.EF.Data;
    using Dayt.EF.Data.Entities;
    using Dayt.EF.Data.Exceptions;
    using Dayt.EF.Entities;
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
        public async Task SaveEvent_ShouldSaveWithoutImageAndRetrieveEventCorrectly()
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
            eventModel.Images.ShouldBeEmpty();
        }

        [Fact]
        public async Task SaveEvent_ShouldSaveWithImageAndRetrieveEventCorrectly()
        {
            // Arrange
            var eventModel = new EventModel
            {
                Title = "Dance event 101",
                Description = "This is a another test event.",
                City = "Berlin",
                Images = new List<EventImageModel>
                {
                    new EventImageModel
                    {
                        ImageType = Data.Enums.ImageTypeEnum.Thumbnail,
                        ImageUrl = "https://dayt.events/images/event101.jpg",
                        Order = 1
                    }
                }
            };

            // Act
            await _repository.SaveEventAsync(eventModel);
            var retrievedEvent = await _repository.GetEventAsync(eventModel.Id);

            // Assert
            retrievedEvent.ShouldNotBeNull();
            eventModel.Title.ShouldBe(retrievedEvent.Title);
            eventModel.Description.ShouldBe(retrievedEvent.Description);
            eventModel.City.ShouldBe(retrievedEvent.City);
            eventModel.Images.ShouldHaveSingleItem();
        }

        [Fact]
        public async Task SaveEvent_NullEvent_ThrowsException()
        {
            // Arrange
            // nothing to arrange 

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _repository.SaveEventAsync(null));
        }
    }
}