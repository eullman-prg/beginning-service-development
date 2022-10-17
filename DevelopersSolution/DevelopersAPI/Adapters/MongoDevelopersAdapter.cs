using DevelopersAPI.Domain;
using MongoDB.Driver;

namespace DevelopersAPI.Adapters;

public class MongoDevelopersAdapter
{

    public IMongoCollection<DeveloperEntity> Developers { get; set; }

    public MongoDevelopersAdapter(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("developers");
        Developers = database.GetCollection<DeveloperEntity>("devs");
    }

}
