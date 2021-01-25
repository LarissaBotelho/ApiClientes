using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApiCobranca.Models
{
    public class Cobranca
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("DataVencimento")]
        [BsonDateTimeOptions]
        [BsonRequired]
        public DateTime DataVencimento { get; set; }

        [BsonElement("Cpf")]
        [BsonRequired]
        [StringLength(11, ErrorMessage = "O Cpf não pode ser maior que 11 dígitos")]
        public string Cpf { get; set; }

        [BsonElement("Valor")]
        [BsonRequired]
        public decimal Valor { get; set; }
    }
}
