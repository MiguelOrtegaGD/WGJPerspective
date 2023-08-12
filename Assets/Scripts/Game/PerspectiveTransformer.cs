using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveTransformer : MonoBehaviour
{
    [SerializeField] GameObject[] topObjects;
    [SerializeField] GameObject[] sideObjects;

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
