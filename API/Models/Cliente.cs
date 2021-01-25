using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StoneAPI.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nome")]
        [BsonRequired]
        public string Nome { get; set; }

        [BsonElement("Estado")]
        [BsonRequired]
        public string Estado { get; set; }

        [BsonElement("Cpf")]
        [BsonRequired]
        [StringLength(11, ErrorMessage = "O Cpf não pode ser maior que 11 dígitos")]
        public string Cpf { get; set; }
    }
}
