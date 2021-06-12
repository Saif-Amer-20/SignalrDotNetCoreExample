using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMarketsCore.AppHelper;
using GlobalMarketsCore.Data;
using GlobalMarketsCore.DataModel;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarketsCore.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _db;
        public ClientService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> ClientExists(int id)
        {
            return await _db.Clients.AnyAsync(e => e.Id == id);
        }

        public async Task<Client> Create(Client client)
        {
            await _db.AddAsync(client);
            await _db.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Details(int clientId)
        {
            return await _db.Clients.AsNoTracking().FirstOrDefaultAsync(l => l.Id == clientId);
        }

       

        public async Task<Client> Edit(Client client)
        {
            _db.Update(client);
            await _db.SaveChangesAsync();
            return client;
        }

        public async Task<Client> GetClient(string email)
        {
            return await _db.Clients.FirstOrDefaultAsync(l => l.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _db.Clients.ToListAsync();
        }

        public List<Client> GetClientsJsonData(int? page, int? limit, string sortBy, string direction, out int total, string firstName, string lastName, int groupId, int statusId, string email, string owner)
        {
            var records = _db.Clients
                .Select(c => new Client()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Age = DateTime.Now.Year - c.DateOfBirth.Year,
                    Email = c.Email,
                    Gender = c.Gender,
                    Mobile = c.Mobile
                })

                .ToArray().AsQueryable();

          
            if (!string.IsNullOrEmpty(firstName))
            {
                records = records.Where(c => c.FirstName.Trim().ToLower().Contains(firstName.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                records = records.Where(c => c.LastName.Trim().ToLower().Contains(lastName.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(email))
            {
                records = records.Where(c => c.Email.Trim().ToLower().Contains(email.Trim().ToLower()));
            }

            total = records.Count();


            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    records = SortHelper.OrderBy(records, sortBy);
                }
                else
                {
                    records = SortHelper.OrderByDescending(records, sortBy);
                }
            }

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value);
            }

            return records.ToList();
        }

        public async Task<bool> Remove(int clientId)
        {
            var item = await _db.Clients.FirstOrDefaultAsync(l => l.Id == clientId);
            _db.Remove(item);
            return await _db.SaveChangesAsync() != 0;
        }

        public async Task<Client> GetClient(string firstName, int categoryId)
        {
            return await _db.Clients.FirstOrDefaultAsync(l => l.FirstName.ToLower().Trim() == firstName.ToLower().Trim());
        }
    }
}
