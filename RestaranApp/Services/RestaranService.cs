using Microsoft.EntityFrameworkCore;
using RestaranApp.Dto;
using RestaranApp.Entities;

namespace RestaranApp.Services
{
    public interface IRestaranService
    {
        public Task Create(CreateRestaranDto r);
        public Task Update(UpdateRestaranDto r);
        public Task Delete(string uuid);

        public Task<List<Restaran>> GetAll();
        public Task<Restaran> GetByUuid(string uuid);
    }

    public class RestaranService : IRestaranService
    {
        private RestaranContext _context;

        public RestaranService(RestaranContext context) {
            _context = context;
        }

        public async Task Create(CreateRestaranDto r)
        {
            Restaran restaran = new Restaran()
            {
                Uuid = Guid.NewGuid().ToString(),
                Name = r.Name,
                Capacity = r.Capacity,
            };

            await _context.Restaran.AddAsync(restaran);
            _context.SaveChanges();
        }

        public async Task Update(UpdateRestaranDto rdata)
        {
            Restaran r = await GetByUuid(rdata.Uuid);
            if (r == null)
            {
                throw new Exception("restaran doesn't exist");
            }

            r.Name = rdata.NewName;
            r.Capacity = rdata.NewCapacity;
            _context.SaveChanges();
        }

        public async Task Delete(string uuid)
        {
            Restaran r = await GetByUuid(uuid);
            if (r == null)
            {
                throw new Exception("restaran doesn't exist");
            }

            _context.Restaran.Remove(r);
            _context.SaveChanges();
        }

        public async Task<List<Restaran>> GetAll()
        {
            return await _context.Restaran.ToListAsync();
        }

        public async Task<Restaran> GetByUuid(string uuid)
        {
            return await _context.Restaran
                .Where(r => r.Uuid == uuid)
                .FirstAsync();
        }
    }
}