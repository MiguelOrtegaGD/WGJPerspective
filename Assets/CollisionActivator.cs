using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionActivator : MonoBehaviour
{
    [SerializeField] string targetTag;

    [SerializeField] UnityEvent finishedActions;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
            finishedActions?.Invoke();
    }
}
