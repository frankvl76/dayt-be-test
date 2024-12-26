using Dayt.EF.Data;
using Dayt.EF.Data.Entities;
using Dayt.EF.Data.Exceptions;
using Dayt.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dayt.EF.Logic
{
    public interface IEventRepository
    {
        Task SaveEventAsync(EventModel model);
        Task<EventModel?> GetEventAsync(int id);
    }

    public class EventRepository : IEventRepository
    {
        private readonly EventDbContext _context;

        public EventRepository(EventDbContext context)
        {
            _context = context;
        }

        public async Task SaveEventAsync(EventModel model)
        {
            if (model is null)
            {
                throw new ValidationException("Event object can't be null");
            }

            //TODO: AutoMapper can be used here!
            var entity = new EventEntity
            {
                Title = model.Title,
                Description = model.Description,
                City = model.City,
                Images = model.Images.Select(i => new EventImageEntity
                {
                    ImageType = i.ImageType,
                    ImageUrl = i.ImageUrl,
                    Order = i.Order
                }).ToList()
            };

            _context.Events.Add(entity);
            await _context.SaveChangesAsync();

            model.Id = entity.Id;
        }

        public async Task<EventModel?> GetEventAsync(int id)
        {
            var entity = await _context.Events.Include("Images").FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null) return null;

            //TODO: AutoMapper can be used here!
            return new EventModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                City = entity.City,
                Images = entity.Images.Select(i => new EventImageModel
                {
                    Id = i.Id,
                    ImageType = i.ImageType,
                    ImageUrl = i.ImageUrl,
                    Order = i.Order
                }).ToList()
            };
        }
    }
}