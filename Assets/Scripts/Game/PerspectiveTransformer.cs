using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerspectiveTransformer : MonoBehaviour
{
    [SerializeField] UnityEvent topActions;
    [SerializeField] UnityEvent sideActions;

    private void OnEnable()
    {
        GameDelegateHelper.changePerspective += ChangePerspective;
    }

    private void OnDisable()
    {
        GameDelegateHelper.changePerspective -= ChangePerspective;
    }

    public void ChangePerspective(PerspectiveEnum newPerspective)
    {
        switch (newPerspective)
        {
            case PerspectiveEnum.Side:
                sideActions?.Invoke();
                break;

            case PerspectiveEnum.Top:
                topActions?.Invoke();
                break;
        }
    }
}
