using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCobranca.Models
{
    public class CobrancaDatabaseSettings : ICobrancaDatabaseSettings
    {
        public string CobrancaCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICobrancaDatabaseSettings
    {
        public string CobrancaCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
