using ApiCobranca.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCobranca.Services
{
    public class CobrancaService
    {

        private readonly IMongoCollection<Cobranca> _cobranca;
        public CobrancaService(ICobrancaDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _cobranca = database.GetCollection<Cobranca>(settings.CobrancaCollectionName);

        }

        public async Task<List<Cobranca>> Get()
        {
            var cobranca = await _cobranca.FindAsync(cob => true);
            return cobranca.ToList();
        }

        public async Task<Cobranca> Get(string id)
        {
            var cobranca = await _cobranca.FindAsync<Cobranca>(cob => cob.Id == id);
            return cobranca.FirstOrDefault();
        }
            

        public async Task<List<Cobranca>> GetByCpf(string cpf)
        {
            var cobranca = await _cobranca.FindAsync<Cobranca>(cob => cob.Cpf == cpf);
            return cobranca.ToList(); 
        }

        public async Task<Cobranca> Create(Cobranca cobranca)
        {   
            await _cobranca.InsertOneAsync(cobranca);
            return cobranca;
        }

        public async Task<Cobranca> Update(string id, Cobranca newCobranca)
        {
           await _cobranca.ReplaceOneAsync(cobranca => cobranca.Id == id, newCobranca);
           return newCobranca;
        }

    }
}

