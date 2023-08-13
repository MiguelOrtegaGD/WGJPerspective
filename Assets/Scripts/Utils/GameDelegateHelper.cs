using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerspectiveEnum { Side, Top }
public static class GameDelegateHelper
{
    public delegate void ChangePerspective(PerspectiveEnum newPerspective);
    public static ChangePerspective changePerspective;

    public delegate void TutorialStep();
    public static TutorialStep nextTutorialStep;
}
