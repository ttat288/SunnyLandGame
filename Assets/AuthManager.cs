using UnityEngine;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    private MongoDBConnection dbConnection;

    void Start()
    {
        string connectionString = "your_connection_string"; // Thay bằng connection string của bạn
        string databaseName = "sunnyland_game";
        dbConnection = new MongoDBConnection(connectionString, databaseName);
    }

    public async void Register(string username, string password)
    {
        bool success = await dbConnection.RegisterUser(username, password);
        if (success)
        {
            Debug.Log("Registration successful!");
        }
        else
        {
            Debug.Log("Username already exists.");
        }
    }

    public async void Login(string username, string password)
    {
        var user = await dbConnection.LoginUser(username, password);
        if (user != null)
        {
            Debug.Log("Login successful!");
        }
        else
        {
            Debug.Log("Invalid username or password.");
        }
    }
}
