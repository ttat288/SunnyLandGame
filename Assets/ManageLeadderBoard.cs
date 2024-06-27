using Assets.Script.singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageLeadderBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nextBtn;
    [SerializeField] int nextScene;
    [SerializeField] int currentScene;
     CurrentScene singletonScene;

    bool chkNextBtn(TextMeshProUGUI btn)
    {
        if (btn.text == "Next")
        {
            return true;
        }
        return false;
    }

    public void Next()
    {
        if(chkNextBtn(nextBtn))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(currentScene);
        }
    }
    private void Awake()
    {
        singletonScene = CurrentScene.Instance; // Ensure singleton is initialized
    }
    public void QuitToMenu()
    {
        singletonScene.CurrentSceneIndex = 2;
        SceneManager.LoadScene(1);
    }
}
