using MongoDB.Driver;
using StoneAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneAPI.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Cliente> _cliente;
        public ClienteService(IClienteDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _cliente = database.GetCollection<Cliente>(settings.ClienteCollectionName);

        }

        public async Task<List<Cliente>> Get()
        {
            var clientes = await _cliente.FindAsync(cliente => true);
            return clientes.ToList();
        }

        public async Task<Cliente> Get(string id)
        {
            var cliente = await _cliente.FindAsync<Cliente>(cliente => cliente.Id == id);
            return cliente.FirstOrDefault();
        }

        public async Task<Cliente> GetByCpf(string cpf)
        {
            var cliente = await _cliente.FindAsync<Cliente>(cliente => cliente.Cpf == cpf);
            return cliente.FirstOrDefault();
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
           await _cliente.InsertOneAsync(cliente);
           return cliente;
        }

    }

}
