using System;
using UnityEngine;
using TMPro; // Add this namespace for TextMeshPro
using System.Threading.Tasks; // Add this namespace for async/await
using Assets.Script.singleton;
using Assets.Script;

public class SceneNav : MonoBehaviour
{
    MongoDBConnection mongoDBManager; // Change this to MongoDBConnection
    public GameObject loginPanel, signupPanel, menuPanel, notificationPanel;

    public TMP_InputField loginUsername, loginPassword, signupUsername, signupPassword, signupCPassword;

    public TMP_Text notif_Title_Text, notif_Message_Text;

    //singleton scene
    static CurrentScene singletonScene;
    static User singletonUser;
    public int currentScreen ;

    private async void Start()
    {
        singletonScene = CurrentScene.Instance; // Ensure singleton is initialized
        currentScreen = singletonScene.CurrentSceneIndex; 

        
        mongoDBManager = new MongoDBConnection();

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
        singletonScene.CurrentSceneIndex = 0;
        ResetPanel();
        loginPanel.SetActive(true);
    }

    public void OpenSignUpPanel()
    {
        currentScreen = 1;
        singletonScene.CurrentSceneIndex = 1;
        ResetPanel();
        signupPanel.SetActive(true);
    }

    public void OpenMenuPanel()
    {
        currentScreen = 2;
        singletonScene.CurrentSceneIndex = 2;
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
        singletonUser = user;
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
