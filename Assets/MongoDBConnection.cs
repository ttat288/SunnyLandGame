using Assets.Script;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

public class MongoDBConnection
{
    private IMongoDatabase database;
    private IMongoCollection<User> usersCollection;

    public MongoDBConnection(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        database = client.GetDatabase(databaseName);
        usersCollection = database.GetCollection<User>("users");
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
}
