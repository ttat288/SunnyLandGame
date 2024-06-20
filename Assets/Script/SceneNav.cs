using System;
using UnityEngine;
using TMPro; // Add this namespace for TextMeshPro
using System.Threading.Tasks; // Add this namespace for async/await

public class SceneNav : MonoBehaviour
{
    MongoDBConnection mongoDBManager; // Change this to MongoDBConnection
    public GameObject loginPanel, signupPanel, menuPanel, notificationPanel;

    public TMP_InputField loginUsername, loginPassword, signupUsername, signupPassword, signupCPassword;

    public TMP_Text notif_Title_Text, notif_Message_Text;

    public int currentScreen = 0;

    private async void Start()
    {
        string connectionString = "mongodb+srv://tranthamanhtoan:Lorenkid113@travis.ocdpowr.mongodb.net/?retryWrites=true&w=majority&appName=Travis";
        string databaseName = "sunnyland_game";
        mongoDBManager = new MongoDBConnection(connectionString, databaseName);

        await Task.Yield(); // Dummy await to prevent the warning

        switch (currentScreen)
        {
            case 0:
                OpenLoginPanel();
                break;
            case 1:
                OpenSignUpPanel();
                break;
            case 2:
                OpenMenuPanel();
                break;
        }
    }


    public void ResetPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        menuPanel.SetActive(false);
    }

    public void OpenLoginPanel()
    {
        currentScreen = 0;
        ResetPanel();
        loginPanel.SetActive(true);
    }

    public void OpenSignUpPanel()
    {
        currentScreen = 1;
        ResetPanel();
        signupPanel.SetActive(true);
    }

    public void OpenMenuPanel()
    {
        currentScreen = 2;
        ResetPanel();
        menuPanel.SetActive(true);
    }

    public async void LoginUser()
    {
        if (string.IsNullOrEmpty(loginUsername.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            ShowNotificationMessage("Error", "Fields Empty! Please Input Details In All Fields");
            return;
        }

        var user = await mongoDBManager.LoginUser(loginUsername.text, loginPassword.text);
        if (user != null)
        {
            ShowNotificationMessage("Success", "Login successful!");
            OpenMenuPanel();
        }
        else
        {
            ShowNotificationMessage("Error", "Invalid username or password");
        }
    }

    public async void SignUpUser()
    {
        if (string.IsNullOrEmpty(signupUsername.text) || string.IsNullOrEmpty(signupPassword.text) || string.IsNullOrEmpty(signupCPassword.text))
        {
            ShowNotificationMessage("Error", "Fields Empty! Please Input Details In All Fields");
            return;
        }

        if (signupPassword.text != signupCPassword.text)
        {
            ShowNotificationMessage("Error", "Passwords do not match");
            return;
        }

        bool success = await mongoDBManager.RegisterUser(signupUsername.text, signupPassword.text);
        if (success)
        {
            ShowNotificationMessage("Success", "Sign up successful! Please log in.");
            OpenLoginPanel();
        }
        else
        {
            ShowNotificationMessage("Error", "Username already exists.");
        }
    }

    private void ShowNotificationMessage(string title, string message)
    {
        notif_Title_Text.text = title;
        notif_Message_Text.text = message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotifPanel()
    {
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";

        notificationPanel.SetActive(false);
    }

    public void Menu()
    {
        OpenMenuPanel();
    }
}
