namespace Dayt.EF.Tests
{
    using Dayt.EF.Data;
    using Dayt.EF.Data.Entities;
    using Dayt.EF.Data.Exceptions;
    using Dayt.EF.Entities;
    using Dayt.EF.Logic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Shouldly;
    using Xunit;

    public class EventImageRepositoryTests
    {
        private readonly EventDbContext _context;
        private readonly EventRepository _eventRepository;
        private readonly EventImageRepository _eventImageRepository;

        public EventImageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EventDbContext>()
                .UseInMemoryDatabase(databaseName: new Guid().ToString())
                .Options;

            _context = new EventDbContext(options);
            _eventRepository = new EventRepository(_context);
            _eventImageRepository = new EventImageRepository(_context);
        }

        [Fact]
        public async Task SaveEventImage_WithEventExist_ShouldCorrectlySaveAndRetrieveEventImage()
        {
            // Arrange
            await CreateSampleEventAsync();

            var eventImageModel = new EventImageModel
            {
                EventId = 1,
                ImageType = Data.Enums.ImageTypeEnum.Thumbnail,
                ImageUrl = "https://dayt.events/images/event101.jpg",
                Order = 1
            };

            // Act
            await _eventImageRepository.SaveEventImageAsync(eventImageModel);
            var retrievedEvent = await _eventImageRepository.GetEventImageAsync(eventImageModel.Id);

            // Assert
            retrievedEvent.ShouldNotBeNull();            
            eventImageModel.ImageUrl.ShouldBe(retrievedEvent.ImageUrl);
            eventImageModel.EventId.ShouldBe(retrievedEvent.EventId);
            eventImageModel.ImageType.ShouldBe(retrievedEvent.ImageType);
            eventImageModel.Order.ShouldBe(retrievedEvent.Order);
        }

        [Fact]
        public async Task SaveEventImage_WithEventDoesNotExist_ShouldNotCorrectlySaveEventImage()
        {
            // Arrange
            var eventImageModel = new EventImageModel
            {
                EventId = 0,
                ImageType = Data.Enums.ImageTypeEnum.Thumbnail,
                ImageUrl = "https://dayt.events/images/event101.jpg",
                Order = 1
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _eventImageRepository.SaveEventImageAsync(eventImageModel));
        }

        [Fact]
        public async Task SaveEventImage_NullEventImage_ThrowsException()
        {
            // Arrange
            // nothing to arrange 

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _eventImageRepository.SaveEventImageAsync(null));
        }

        [Fact]
        public async Task GetEventImages_NonExistentId_ReturnsNull()
        {
            // Arrange
            // nothing to arrange

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _eventImageRepository.GetEventImageAsync(0));   // Event Image ID does not exist
        }

        private async Task CreateSampleEventAsync()
        {
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

            await _eventRepository.SaveEventAsync(eventModel);
        }
    }
}