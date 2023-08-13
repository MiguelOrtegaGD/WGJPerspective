using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MatrixBlender : MonoBehaviour
{
    Camera currentCamera;

    [SerializeField] float transitionDuration;
    private void Start()
    {
        currentCamera = GetComponent<Camera>();

        // Define la matriz de proyecci�n en perspectiva que deseas aplicar
        Matrix4x4 perspectiveMatrix = Matrix4x4.Perspective(60, GetComponent<Camera>().aspect, GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);

        // Llama a la funci�n BlendToMatrix para realizar la transici�n
        MatrixBlender matrixBlender = GetComponent<MatrixBlender>();
        matrixBlender.BlendToMatrix(perspectiveMatrix, transitionDuration);
    }

    public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        return ret;
    }

    private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            currentCamera.projectionMatrix = MatrixLerp(src, dest, (Time.time - startTime) / duration);
            yield return 1;
        }
        currentCamera.projectionMatrix = dest;
    }

    public Coroutine BlendToMatrix(Matrix4x4 targetMatrix, float duration)
    {
        StopAllCoroutines();
        return StartCoroutine(LerpFromTo(currentCamera.projectionMatrix, targetMatrix, duration));
    }
}
