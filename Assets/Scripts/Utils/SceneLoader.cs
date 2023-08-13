using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string musicName;
    private void Start()
    {
        if (!string.IsNullOrEmpty(musicName))
            AudioManager.Instance.PlayMainMusic(musicName);
    }
}
