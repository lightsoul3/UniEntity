using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NoSql.Database;

namespace NoSql.Services
{
    public class ServiceConnection
    {
        public IMongoDatabase MongoDatabase { get; }

        public ServiceConnection(IOptions<DbConnection> db)
        {
            var mongoClient = new MongoClient(db.Value.ConnectionString);
            MongoDatabase = mongoClient.GetDatabase(db.Value.Database);
        }
    }
}
