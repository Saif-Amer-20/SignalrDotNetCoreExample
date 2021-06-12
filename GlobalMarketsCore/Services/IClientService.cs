using GlobalMarketsCore.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMarketsCore.Services
{
    public interface IClientService
    {
        Task<Client> Create(Client client);
        Task<Client> Edit(Client client);
        Task<bool> Remove(int clientId);
        Task<Client> Details(int clientId);
        Task<Client> GetClient(string email);
        Task<IEnumerable<Client>> GetClients();
        List<Client> GetClientsJsonData(int? page, int? limit, string sortBy, string direction, out int total, string firstName, string lastName, int groupId, int statusId , string email, string owner);
        Task<bool> ClientExists(int id);
        Task<Client> GetClient(string firstName, int categoryId);
    }
}
