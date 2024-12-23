using Dayt.EF.Data;
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
            var entity = new EventEntity
            {
                Title = model.Title,
                Description = model.Description,
                City = model.City
            };

            _context.Events.Add(entity);
            await _context.SaveChangesAsync();

            model.Id = entity.Id;
        }

        public async Task<EventModel?> GetEventAsync(int id)
        {
            var entity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null) return null;

            return new EventModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                City = entity.City
            };
        }
    }
}