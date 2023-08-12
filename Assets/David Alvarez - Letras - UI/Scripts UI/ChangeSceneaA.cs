using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneA: MonoBehaviour
{
    public int tagScene = 0;
    public void loadScene()
    {
        SceneManager.LoadScene(tagScene);
    }

    public void ReloadLevel()
    {
        Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}