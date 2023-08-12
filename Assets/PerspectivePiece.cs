using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePiece : MonoBehaviour
{
    bool changeScale = false;
    float lastScale;
    float newScale;
    [SerializeField] float scaleSpeed;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] float duration;

    [SerializeField] ObjectScaler scaler;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (changeScale)
        {
            if (transform.localScale != new Vector3(newScale, newScale, newScale))
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(newScale, newScale, newScale), scaleSpeed * Time.deltaTime);

            else
                changeScale = false;
        }
    }

    public void ChangePerspective(PerspectiveEnum newPerspective)
    {
        switch (newPerspective)
        {
            case PerspectiveEnum.Side:
                ChangeScale();
                break;

            case PerspectiveEnum.Top:
                ReturnScale();
                break;
        }
    }

    public void ChangeScale()
    {
        lastScale = transform.localScale.x;
        newScale = scaler.ConvertRange(transform.position.z, minScale, maxScale);
        scaleSpeed = Mathf.Abs((transform.localScale.x - newScale)) / duration;
        changeScale = true;
    }

    public void ReturnScale()
    {
        lastScale = transform.localScale.x;
        newScale = 1;
        scaleSpeed = Mathf.Abs((transform.localScale.x - newScale)) / duration;
        changeScale = true;
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
