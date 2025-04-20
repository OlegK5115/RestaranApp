using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaranApp.Dto;
using RestaranApp.Entities;
using System;
using System.IO.Pipes;
using System.Xml.Linq;

namespace RestaranApp.Services
{
    public interface IOrderService
    {
        public Task Create(CreateOrderDto order);

        public Task<Order> GetByUuid(string uuid);
        public Task<List<Order>> GetAll();
        public Task<List<Order>> GetByUser(string useruuid);
        public Task<List<Order>> GetByRestaran(string restaranuuid);
    };

    public class OrderService : IOrderService
    {
        private RestaranContext _context;
        private IUserService _userService;
        private IRestaranService _restaranService;

        public OrderService(RestaranContext context, IUserService us, IRestaranService rs)
        {
            _context = context;
            _userService = us;
            _restaranService = rs;
        }

        private async Task<List<Order>> GetAllByDateTime(string uuid, DateTime startTime, DateTime endTime)
        {
            List<Order> ans = await _context.Order
                .Where(e => e.Restaran.Uuid == uuid)
                .Where(e => e.StartTime < endTime && e.EndTime > startTime)
                .ToListAsync();

            return ans;
        }

        private async Task<bool> CheckCapacity(CreateOrderDto o)
        {
            Restaran r = await _restaranService.GetByUuid(o.RestaranUuid);
            List<Order> orders = await GetAllByDateTime(r.Uuid, o.StartTime, o.EndTime);

            TimeSpan t = o.EndTime - o.StartTime;
            for (int i = 0; i < t.Hours; i++)
            {
                DateTime datetime = o.StartTime.AddHours(i);

                int count = orders.Count(order => order.StartTime <= datetime && order.EndTime > datetime);
                if (count >= r.Capacity)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task Create(CreateOrderDto o)
        {
            User u = await _userService.GetByUuid(o.UserUuid);
            if (u == null) {
                throw new Exception("user doesn't exist");
            }

            Restaran r = await _restaranService.GetByUuid(o.RestaranUuid);
            if (r == null)
            {
                throw new Exception("restaran doesn't exist");
            }

            if (!await CheckCapacity(o))
            {
                throw new Exception("wrong datetime");
            }

            Order order = new Order()
            {
                Uuid = Guid.NewGuid().ToString(),
                UserId = u.Id,
                RestaranId = r.Id,
                StartTime = o.StartTime,
                EndTime = o.EndTime
            };

            await _context.Order.AddAsync(order);
            _context.SaveChanges();

            int orderscount = (await GetByUser(u.Uuid)).Count;
            await _userService.UpdateStatus(u.Uuid, orderscount);
        }

        public async Task<Order> GetByUuid(string uuid)
        {
            return await _context.Order.Where(o => o.Uuid == uuid).FirstAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<List<Order>> GetByUser(string useruuid)
        {
            var orders = await _context.Order.Where(o => o.User.Uuid == useruuid).ToListAsync();
            /*  равносильно join запросу:
             *  select * from Orders o
             *  join Users u on u.Id = o.UserId
             *  where u.Username = "username";
             */

            return orders;
        }

        public async Task<List<Order>> GetByRestaran(string restaranuuid)
        {
            var orders = await _context.Order
                .Where(o => o.Restaran.Uuid == restaranuuid)
                .ToListAsync();
            /*  равносильно join запросу:
             *  select * from Orders o
             *  join Users u on u.Id = o.UserId
             *  where u.Username = "username";
             */

            return orders;
        }
    }
}