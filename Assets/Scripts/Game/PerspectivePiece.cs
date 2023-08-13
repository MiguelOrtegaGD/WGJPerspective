using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePiece : MonoBehaviour
{
    bool changeScale = false;
    float newScale;
    [SerializeField] float scaleSpeed;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] float duration;

    [SerializeField] float rayDistance;
    [SerializeField] LayerMask rayLayers;

    [SerializeField] ObjectScaler scaler;
    [SerializeField] GameObject child;

    [SerializeField] Vector3 childSize;

    float initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale.x;
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
        newScale = scaler.ConvertRange(child.transform.position.z, maxScale, minScale, childSize.z);

        Ray positiveXRay = new Ray(new Vector3(child.transform.position.x + (newScale / 2), child.transform.position.y, child.transform.position.z), Vector3.down);
        Ray negativeXRay = new Ray(new Vector3(child.transform.position.x - (newScale / 2), child.transform.position.y, child.transform.position.z), Vector3.down);
        Ray positiveZRay = new Ray(new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z + (newScale / 2)), Vector3.down);
        Ray negativeZRay = new Ray(new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z - (newScale / 2)), Vector3.down);

        bool positiveX = Physics.Raycast(positiveXRay, rayDistance, rayLayers);
        bool negativeX = Physics.Raycast(negativeXRay, rayDistance, rayLayers);
        bool positiveZ = Physics.Raycast(positiveZRay, rayDistance, rayLayers);
        bool negativeZ = Physics.Raycast(negativeZRay, rayDistance, rayLayers);

        child.transform.SetParent(null);
        Vector3 newPivotPosition = new Vector3(0, child.transform.position.y - (childSize.y / 2), 0);

        if (positiveX && negativeX)
            newPivotPosition.x = child.transform.position.x;
        else if (positiveX)
            newPivotPosition.x = child.transform.position.x - (childSize.x / 2);
        else if (negativeX)
            newPivotPosition.x = child.transform.position.x + (childSize.x / 2);

        if (positiveZ && negativeZ)
            newPivotPosition.z = child.transform.position.z;
        else if (positiveZ)
            newPivotPosition.z = child.transform.position.z - (childSize.z / 2);
        else if (negativeZ)
            newPivotPosition.z = child.transform.position.z + (childSize.z / 2);

        transform.position = newPivotPosition;
        child.transform.SetParent(transform);

        scaleSpeed = Mathf.Abs((transform.localScale.x - newScale)) / duration;
        changeScale = true;
    }

    public void ReturnScale()
    {
        newScale = initialScale;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(child.transform.position, childSize);

        Gizmos.DrawLine(new Vector3(child.transform.position.x - (newScale / 2), child.transform.position.y, child.transform.position.z), new Vector3(child.transform.position.x - (newScale / 2), child.transform.position.y - rayDistance, child.transform.position.z));
        Gizmos.DrawLine(new Vector3(child.transform.position.x + (newScale / 2), child.transform.position.y, child.transform.position.z), new Vector3(child.transform.position.x + (newScale / 2), child.transform.position.y - rayDistance, child.transform.position.z));
        Gizmos.DrawLine(new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z - (newScale / 2)), new Vector3(child.transform.position.x, child.transform.position.y - rayDistance, child.transform.position.z - (newScale / 2)));
        Gizmos.DrawLine(new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z + (newScale / 2)), new Vector3(child.transform.position.x, child.transform.position.y - rayDistance, child.transform.position.z + (newScale / 2)));
    }
}
