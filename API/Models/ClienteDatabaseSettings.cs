namespace StoneAPI.Models
{
    public class ClienteDatabaseSettings : IClienteDatabaseSettings
        {
            public string ClienteCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IClienteDatabaseSettings
        {
            public string ClienteCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }
}
