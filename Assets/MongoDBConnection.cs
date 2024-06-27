using Assets.Script;
using Assets.Script.singleton;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MongoDBConnection
{
    private IMongoDatabase database;
    private IMongoCollection<User> usersCollection;
    private IMongoCollection<HighscoreEntry> rankCollection;
    string connectionString = "mongodb+srv://tranthamanhtoan:Lorenkid113@travis.ocdpowr.mongodb.net/?retryWrites=true&w=majority&appName=Travis";
    string databaseName = "sunnyland_game";

    public MongoDBConnection()
    {
        var client = new MongoClient(connectionString);
        database = client.GetDatabase(databaseName);
        usersCollection = database.GetCollection<User>("users");
        rankCollection = database.GetCollection<HighscoreEntry>("scores");
    }

    public async Task<bool> RegisterUser(string username, string password)
    {
        var existingUser = await usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            return false; // User already exists
        }

        var newUser = new User
        {
            Username = username,
            Password = password // Note: In a real application, make sure to hash the password
        };

        await usersCollection.InsertOneAsync(newUser);
        return true;
    }

    public async Task<User> LoginUser(string username, string password)
    {
        var user = await usersCollection.Find(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        return user;
    }

    public async Task<List<HighscoreEntry>> GetTopRank(string id)
    {
        var top9 = await rankCollection.Find(Builders<HighscoreEntry>.Filter.Empty)
                                       .Sort(Builders<HighscoreEntry>.Sort.Descending(e => e.playerScore))
                                       .Limit(9)
                                       .ToListAsync();

        var player = await rankCollection.Find(e => e.playerId == id).FirstOrDefaultAsync();

        if (player != null)
        {
            top9.Add(player);
        }

        return top9;
    }

    public async Task UpdateScore(string id, int newScore)
    {
        var filter = Builders<HighscoreEntry>.Filter.Eq(entry => entry.playerId, id);
        var update = Builders<HighscoreEntry>.Update.Set(entry => entry.playerScore, newScore);
        await rankCollection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }

    public async Task<string> getNameById(string id)
    {
        var user = await usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        return user?.Username;
    }
}
