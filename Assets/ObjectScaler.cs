using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] float size;
    [SerializeField] Transform obj;

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, areaSize);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - size));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Debug.Log(MapValueToRange(obj.position.z, transform.position.z - size, transform.position.z, 0, 1));
        }
    }

    public float ConvertRange(float valueA, float minB, float maxB)
    {
        // Asegurarse de que valueA esté dentro del rango [minA, maxA]
        valueA = Mathf.Clamp(valueA, transform.position.z - size, transform.position.z);

        // Calcular el valor normalizado en el rango A
        float normalizedValueA = (valueA - transform.position.z - size) / (transform.position.z - transform.position.z - size);

        // Calcular el valor equivalente en el rango B
        float valueB = Mathf.Lerp(minB, maxB, normalizedValueA);

        return valueB;
    }
}
