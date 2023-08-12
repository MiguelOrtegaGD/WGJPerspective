using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] float size;
    [SerializeField] Transform obj;

    public float Size { get => size; set => size = value; }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, areaSize);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - size));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Debug.Log(ConvertRange(obj.position.z, 2, 1));
        }
    }

    public float ConvertRange(float valueA, float maxB, float minB, float objectZSize)
    {
        maxB = -maxB;
        float minA = (transform.position.z - size) + (objectZSize / 2);
        float maxA = transform.position.z - (objectZSize / 2);

        // Asegurarse de que valueA esté dentro del rango [minA, maxA]
        valueA = Mathf.Clamp(valueA, minA, maxA);

        // Calcular el valor normalizado en el rango A
        float normalizedValueA = (valueA - minA) / (maxA - minA);

        // Calcular el valor equivalente en el rango B
        float valueB = Mathf.Lerp(maxB, minB, normalizedValueA);

        return Mathf.Abs(valueB);
    }
}
