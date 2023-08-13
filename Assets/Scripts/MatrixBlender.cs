using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MatrixBlender : MonoBehaviour
{
    Camera currentCamera;
    [SerializeField] Camera perspectiveCamera;
    Matrix4x4 targetMatrix;
    float transitionDuration;
    float transitionProgress;
    bool transitioning = false;
    Matrix4x4 orthographicMatrix;
    Matrix4x4 perspectiveMatrix;
    [SerializeField] float transitionSpeed = 1.0f;

    private void Start()
    {
        currentCamera = GetComponent<Camera>();
        transitionDuration = 1.0f; // Tiempo de transición inicial

        //orthographicMatrix = Matrix4x4.Ortho(-10, 10, -10, 10, currentCamera.nearClipPlane, currentCamera.farClipPlane);
        orthographicMatrix = currentCamera.projectionMatrix;
        perspectiveMatrix = Matrix4x4.Perspective(perspectiveCamera.fieldOfView, perspectiveCamera.aspect, perspectiveCamera.nearClipPlane, perspectiveCamera.farClipPlane);
        perspectiveCamera.gameObject.SetActive(false);
        // = Matrix4x4.Perspective(60, GetComponent<Camera>().aspect, GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);
        targetMatrix = orthographicMatrix;
    }

    private void Update()
    {
        if (transitioning)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            if (transitionProgress > 1.0f)
            {
                transitionProgress = 1.0f;
                transitioning = false;
            }

            currentCamera.projectionMatrix = MatrixLerp(currentCamera.projectionMatrix, targetMatrix, transitionProgress);
        }
    }

    public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        return ret;
    }

    public void BlendToMatrix(Matrix4x4 newMatrix, float duration)
    {
        targetMatrix = newMatrix;
        transitionDuration = duration;
        transitionProgress = 0.0f;
        transitioning = true;
    }

    public void ChangePerspective(PerspectiveEnum newPerspective)
    {
        //BlendToMatrix(orthographicMatrix, transitionSpeed);
        BlendToMatrix(newPerspective == PerspectiveEnum.Side ? orthographicMatrix : perspectiveMatrix, transitionSpeed);
    }

    private void OnEnable()
    {
        GameDelegateHelper.changePerspective += ChangePerspective;
    }

    private void OnDisable()
    {
        GameDelegateHelper.changePerspective -= ChangePerspective;
    }

}
