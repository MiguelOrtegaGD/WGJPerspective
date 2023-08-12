using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    Slider _sliderController;
    [SerializeField] bool _music;

    void Start()
    {
        _sliderController = GetComponent<Slider>();

        _sliderController.minValue = -80;
        _sliderController.maxValue = 0;

        UpdateSliderValue();

        _sliderController.onValueChanged.AddListener(
            delegate
            {
                AudioManager.Instance.ChangeVolume(_music, _sliderController.value);
            }
        );
    }

    private void OnEnable()
    {
        AudioManager.Instance.soundVolumeIsChanged += UpdateSliderValue;
    }

    public void UpdateSliderValue()
    {
        _sliderController.value = AudioManager.Instance.GetVolume(_music);
    }
}
