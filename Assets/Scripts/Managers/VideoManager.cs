using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VideoManager : Singleton<VideoManager>
{
    Resolution[] _availableResolutions;
    bool _fullScreen;

    public delegate void FullScreenState();
    public event FullScreenState fullScreenStateChanged;
    private void Awake()
    {
        _availableResolutions = Screen.resolutions;
    }
    private void Start()
    {
        //Screen.resol
        //QualitySettings.;
    }

    public List<string> ResolutionOptions()
    {
        List<string> _resolutionList = new List<string>();

        foreach (Resolution resolution in _availableResolutions)
        {
            _resolutionList.Add($"{resolution.width}x{resolution.height}");
        }

        return _resolutionList;
    }

    public void ChangeResolution(int _index)
    {
        Screen.SetResolution(_availableResolutions[_index].width, _availableResolutions[_index].height, Screen.fullScreen);
    }

    public void ChangeFullScreenState()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullScreenStateChanged?.Invoke();
    }

    public int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < _availableResolutions.Length; i++)
        {
            if (_availableResolutions[i].width == Screen.currentResolution.width && _availableResolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }

        return 0;
    }

    public List<string> QualityOptions()
    {
        List<string> _qualityList = new List<string>();

        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            _qualityList.Add(QualitySettings.names[i]);
        }

        return _qualityList;
    }

    public void ChangeQuality(int _index)
    {
        QualitySettings.SetQualityLevel(_index);
    }

    public int GetCurrentQualityIndex()
    {
        return QualitySettings.GetQualityLevel();
    }
}
