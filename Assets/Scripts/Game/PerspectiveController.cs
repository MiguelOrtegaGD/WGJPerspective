using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerspectiveController : MonoBehaviour
{
    [SerializeField] PerspectiveEnum currentPerspective;

    [SerializeField] UnityEvent topObjects;
    [SerializeField] UnityEvent sideObjects;

    //private void Awake()
    //{
    //    GameDelegateHelper.changePerspective?.Invoke(currentPerspective);

    //    if (currentPerspective == PerspectiveEnum.Top)
    //        topObjects?.Invoke();
    //    else
    //        sideObjects?.Invoke();
    //}

    private void Start()
    {
        GameDelegateHelper.changePerspective?.Invoke(currentPerspective);

        if (currentPerspective == PerspectiveEnum.Top)
            topObjects?.Invoke();
        else
            sideObjects?.Invoke();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangePerspective();
        }
    }

    public void ChangePerspective()
    {
        currentPerspective = currentPerspective == PerspectiveEnum.Top ? PerspectiveEnum.Side : PerspectiveEnum.Top;
        GameDelegateHelper.changePerspective?.Invoke(currentPerspective);

        if (currentPerspective == PerspectiveEnum.Top)
            topObjects?.Invoke();
        else
            sideObjects?.Invoke();
    }
}
