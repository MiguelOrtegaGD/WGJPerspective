using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TutorialInputEnum { Collision, Input, Cinematic, Time }
public class TutorialController : MonoBehaviour
{
    int currentStep = 0;
    [SerializeField] GameObject[] tutorialSteps;
    private void Start()
    {
        tutorialSteps[currentStep].SetActive(true);
    }

    public void NextStep()
    {
        currentStep++;

        if (currentStep < tutorialSteps.Length)
            tutorialSteps[currentStep].SetActive(true);
    }

    private void OnEnable()
    {
        GameDelegateHelper.nextTutorialStep += NextStep;
    }

    private void OnDisable()
    {
        GameDelegateHelper.nextTutorialStep -= NextStep;
    }
}
