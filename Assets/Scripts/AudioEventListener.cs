using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventListener : MonoBehaviour
{
    [SerializeField] List<string> soundEffects = new List<string>();
    public void PlaySoundEffect()
    {
        int random = Random.Range(0, soundEffects.Count);
        AudioManager.Instance.PlayMainSfx(soundEffects[random]);
    }

    public void LandEffect()
    {
        AudioManager.Instance.PlayMainSfx("Landing");
    }

    public void JumpEffect()
    {
        AudioManager.Instance.PlayMainSfx("Jump");
    }
}
