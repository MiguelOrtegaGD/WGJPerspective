using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    [SerializeField] KeyCode key;
    [SerializeField] UnityEvent actions;

    private void Update()
    {
        if (Input.GetKeyDown(key))
            actions?.Invoke();
    }
}
