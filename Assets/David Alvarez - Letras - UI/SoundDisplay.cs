using UnityEngine;
using UnityEngine.UI;

public class SoundDisplay : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image soundImage;

    private bool isSoundEnabled = true;

    private void Start()
    {
        UpdateSoundDisplay();
    }

    public void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        UpdateSoundDisplay();
        AudioListener.volume = isSoundEnabled ? 1.0f : 0.0f;
    }

    private void UpdateSoundDisplay()
    {
        soundImage.sprite = isSoundEnabled ? soundOnSprite : soundOffSprite;
    }
}
