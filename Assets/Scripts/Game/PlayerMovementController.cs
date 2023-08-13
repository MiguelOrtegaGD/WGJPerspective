using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

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
                GetComponent<PlayerMovementSide>().enabled = true;
                GetComponent<PlayerMovementTop>().enabled = false;
                break;

            case PerspectiveEnum.Top:
                GetComponent<PlayerMovementSide>().enabled = false;
                GetComponent<PlayerMovementTop>().enabled = true;
                break;
        }
    }
}
