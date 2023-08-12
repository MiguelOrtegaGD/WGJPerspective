using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoOptionsDropdown : MonoBehaviour
{
    TMP_Dropdown _dropdown;
    Resolution[] _resolutions;

    [SerializeField] bool _quality;

    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        _dropdown.ClearOptions();
        _dropdown.AddOptions(_quality ? VideoManager.Instance.QualityOptions() : VideoManager.Instance.ResolutionOptions());

        _dropdown.onValueChanged.AddListener(delegate
        {
            if (_quality)
                VideoManager.Instance.ChangeQuality(_dropdown.value);
            else
                VideoManager.Instance.ChangeResolution(_dropdown.value);
        });

        _dropdown.value = _quality ? VideoManager.Instance.GetCurrentQualityIndex() : VideoManager.Instance.GetCurrentResolutionIndex();
    }
}
