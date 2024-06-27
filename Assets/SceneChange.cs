using Assets.Script;
using Assets.Script.singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;
    MongoDBConnection connection;
    User user;
    CurrentScene currentScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentScene = CurrentScene.Instance;
        //khi end game thi add xuong db
        user = User.Instance;
        connection = new MongoDBConnection();
        connection.UpdateScore(user.Id, currentScene.CurrentCherry);

        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
