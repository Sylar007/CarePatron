using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public interface IClientRepository
    {
        Task<Client[]> Get();
        Task Create(Client client);
        Task Update(Client client);
        Task<Client?> GetById(string id);
        Task<Client[]> GetByText(string filterText);
    }
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext dataContext;

        public ClientRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Create(Client client)
        {
            var existingClient = await dataContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);
            if (existingClient is not null)
                throw new InvalidOperationException("Id has already created. Please enter different Id");

            await dataContext.AddAsync(client);
            await dataContext.SaveChangesAsync();
        }

        public Task<Client[]> Get()
        {
            return dataContext.Clients.ToArrayAsync();
        }

        public async Task Update(Client client)
        {
            var existingClient = await dataContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);

            if (existingClient == null)
                throw new InvalidOperationException("Client is not found");

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;

            await dataContext.SaveChangesAsync();
        }

        public async Task<Client?> GetById(string id)
        {
            var findClient = await dataContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
            return findClient;
        }

        public async Task<Client[]> GetByText(string filterText)
        {
            var filterClients = await dataContext.Clients.Where(x => x.FirstName.ToLower().Contains(filterText.ToLower()) || 
                                x.LastName.ToLower().Contains(filterText.ToLower())).ToArrayAsync();
            return filterClients;
        }
    }
}

