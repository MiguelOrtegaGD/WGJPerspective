using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static string soundPrefKey = "SoundPref";
    public Image soundOnImage;
    public Image soundOffImage;

    // Inicializamos sound con true por defecto (sonido activado)
    private static bool sound = true;

    private void Start()
    {
        // Recuperar el estado del sonido de PlayerPrefs al inicio
        sound = PlayerPrefs.GetInt(soundPrefKey, 1) == 1;
        UpdateSoundImage();

        // Llamar al método para cambiar el volumen global del audio
        AudioListener.volume = sound ? 1.0f : 0.0f;
    }

    public void ToggleSound()
    {
        sound = !sound;
        UpdateSoundImage();

        // Llamar al método para cambiar el volumen global del audio
        AudioListener.volume = sound ? 1.0f : 0.0f;

        // Guardar el estado del sonido en PlayerPrefs para mantenerlo entre escenas
        PlayerPrefs.SetInt(soundPrefKey, sound ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateSoundImage()
    {
        if (sound)
        {
            soundOnImage.gameObject.SetActive(true);
            soundOffImage.gameObject.SetActive(false);
        }
        else
        {
            soundOnImage.gameObject.SetActive(false);
            soundOffImage.gameObject.SetActive(true);
        }
    }
}
