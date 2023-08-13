using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveController : MonoBehaviour
{
    [SerializeField] PerspectiveEnum currentPerspective;

    [SerializeField] GameObject[] topObjects;
    [SerializeField] GameObject[] sideObjects;

    private void Awake()
    {
        currentPerspective = PerspectiveEnum.Top; 
        ChangePerspective();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePerspective();
        }
    }

    public void ChangePerspective()
    {
        currentPerspective = currentPerspective == PerspectiveEnum.Top ? PerspectiveEnum.Side : PerspectiveEnum.Top;
        GameDelegateHelper.changePerspective?.Invoke(currentPerspective);
        ChangePerspectiveObjects();
    }

    public void ChangePerspectiveObjects()
    {
        switch (currentPerspective)
        {
            case PerspectiveEnum.Side:
                foreach (var item in topObjects)
                    item.SetActive(false);
                foreach (var item in sideObjects)
                    item.SetActive(true);
                break;

            case PerspectiveEnum.Top:
                foreach (var item in sideObjects)
                    item.SetActive(false);
                foreach (var item in topObjects)
                    item.SetActive(true);
                break;
        }
    }
}
