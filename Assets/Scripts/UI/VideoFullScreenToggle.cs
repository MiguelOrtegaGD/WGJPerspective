using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoFullScreenToggle : MonoBehaviour
{
    Toggle _toggle;

    // Start is called before the first frame update
    void Start()
    {
        _toggle = GetComponent<Toggle>();

        _toggle.onValueChanged.AddListener(delegate
        {
            VideoManager.Instance.ChangeFullScreenState();
        });

        UpdateToggleState();
    }

    private void OnEnable()
    {
        VideoManager.Instance.fullScreenStateChanged += UpdateToggleState;
    }

    public void UpdateToggleState()
    {
        _toggle.isOn = !Screen.fullScreen;
    }
}
