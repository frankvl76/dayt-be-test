using Dayt.EF.Data;
using Dayt.EF.Data.Entities;
using Dayt.EF.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Dayt.EF.Logic
{
    public interface IEventImageRepository
    {
        Task SaveEventImageAsync(EventImageModel model);
        Task<EventImageModel?> GetEventImageAsync(int id);
    }

    public class EventImageRepository : IEventImageRepository
    {
        private readonly EventDbContext _context;

        public EventImageRepository(EventDbContext context)
        {
            _context = context;
        }

        public async Task SaveEventImageAsync(EventImageModel model)
        {
            if (model is null)
            {
                throw new ValidationException("Event Image object can't be null");
            }

            EventModel? eventModel = await new EventRepository(_context).GetEventAsync(model.EventId);

            if(eventModel is null)
            {
                throw new NotFoundException("Event not found"); 
            }

            //TODO: AutoMapper can be used here!
            var entity = new EventImageEntity
            {
                EventId = model.EventId,
                ImageType = model.ImageType,
                ImageUrl = model.ImageUrl,
                Order = model.Order
            };

            _context.EventImages.Add(entity);
            await _context.SaveChangesAsync();

            model.Id = entity.Id;
        }

        public async Task<EventImageModel?> GetEventImageAsync(int id)
        {
            var entity = await _context.EventImages.Include("Event").FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new NotFoundException("Event image not found");
            }

            //TODO: AutoMapper can be used here!
            return new EventImageModel
            {
                Id = entity.Id,
                EventId = entity.EventId,
                ImageType = entity.ImageType,
                ImageUrl = entity.ImageUrl,
                Order = entity.Order
            };
        }
    }
}