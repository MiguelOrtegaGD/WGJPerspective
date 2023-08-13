using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameFinishConsumer : MonoBehaviour
{
    [SerializeField] string targetTag;

    [SerializeField] UnityEvent finishedActions;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
            FinishGame();
    }

    public void FinishGame()
    {
        finishedActions?.Invoke();
        GameDelegateHelper.isLevelFinished?.Invoke();
    }
}
