using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtils : MonoBehaviour
{


    public void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneWithFade(string sceneName)
    {
        StartCoroutine(ChangeSceneWithFadeCoroutine(sceneName));
    }

    public IEnumerator ChangeSceneWithFadeCoroutine(string sceneName)
    {
        UIController.Instance.Fade(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadSceneWithFade()
    {
        StartCoroutine(ChangeSceneWithFadeCoroutine(SceneManager.GetActiveScene().name));
    }
}
