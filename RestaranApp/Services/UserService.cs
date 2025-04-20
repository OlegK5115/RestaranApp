using Microsoft.EntityFrameworkCore;
using RestaranApp.Dto;
using RestaranApp.Entities;

namespace RestaranApp.Services
{
    public interface IUserService
    {
        public Task Create(CreateUserDto u);
        public Task Update(UpdateUserDto u);
        public Task UpdateStatus(string uuid, int oderscount);
        public Task Delete(string uuid);

        public Task<List<User>> GetAll();
        public Task<User> GetByUuid(string uuid);
    }

    public class UserService : IUserService
    {
        private RestaranContext _context;

        public UserService(RestaranContext context)
        {
            _context = context;
        }

        public async Task Create(CreateUserDto u)
        {
            User user = new User()
            {
                Uuid = Guid.NewGuid().ToString(),
                Name = u.Name,
                Email = u.Email,
                Status = "SimpleClient"
            };

            await _context.User.AddAsync(user);
            _context.SaveChanges();
        }

        public async Task Update(UpdateUserDto udata)
        {
            User u = await GetByUuid(udata.Uuid);
            if (u == null)
            {
                throw new Exception("user doesn't exist");
            }

            u.Name = udata.NewName;
            _context.SaveChanges();
        }

        public async Task UpdateStatus(string uuid, int oderscount)
        {
            User u = await GetByUuid(uuid);

            if (oderscount > 10)
            {
                u.Status = "PremiumClient";
                _context.SaveChanges();
            }
        }

        public async Task Delete(string uuid)
        {
            User u = await GetByUuid(uuid);
            if (u == null)
            {
                throw new Exception("user doesn't exist");
            }

            _context.User.Remove(u);
            _context.SaveChanges();
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetByUuid(string uuid)
        {
            return await _context.User.Where(u => u.Uuid == uuid).FirstAsync();
        }
    }
}