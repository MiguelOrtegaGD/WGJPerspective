using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMuteButton : MonoBehaviour
{
    [SerializeField] bool _music;
    Button _button;
    Image _image;

    [SerializeField] Sprite _muteSprite, _unmuteSprite;

    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _button.onClick.AddListener(() => MuteState());
    }

    private void OnEnable()
    {
        AudioManager.Instance.soundVolumeIsChanged += UpdateMuteSprite;
    }

    public void MuteState()
    {
        if (AudioManager.Instance.IsMuted(_music))
            AudioManager.Instance.Unmute(_music);
        else
            AudioManager.Instance.Mute(_music);

        UpdateMuteSprite();
    }

    public void UpdateMuteSprite()
    {
        _image.sprite = AudioManager.Instance.IsMuted(_music) ? _muteSprite : _unmuteSprite;
    }
}
