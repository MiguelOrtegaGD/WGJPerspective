using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] float size;
    //[SerializeField] Transform obj;

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
        float difference = Mathf.Abs(transform.position.z - transform.position.z - size);

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
    public float ConvertirValor(float valorZActual, float rangoDestinoMin, float rangoDestinoMax, float objectZSize)
    {
        float rangoOriginalMin = (transform.position.z - size) + (objectZSize / 2);
        float rangoOriginalMax = transform.position.z - (objectZSize / 2);

        // Asegurarse de que el valorZActual esté dentro del rangoOriginal
        valorZActual = Mathf.Clamp(valorZActual, rangoOriginalMin, rangoOriginalMax);

        // Calcular la proporción del valorZActual en el rangoOriginal
        float proporcion = (valorZActual - rangoOriginalMin) / (rangoOriginalMax - rangoOriginalMin);

        // Convertir la proporción al rangoDestino
        float valorConvertido = rangoDestinoMin + proporcion * (rangoDestinoMax - rangoDestinoMin);

        Debug.Log(valorConvertido);
        return valorConvertido;
    }
}
